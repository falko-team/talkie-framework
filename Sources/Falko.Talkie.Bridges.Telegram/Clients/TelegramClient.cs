using System.IO.Compression;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Falko.Talkie.Bridges.Telegram.Configurations;
using Falko.Talkie.Bridges.Telegram.Exceptions;
using Falko.Talkie.Bridges.Telegram.Models;
using Falko.Talkie.Bridges.Telegram.Policies;
using Falko.Talkie.Bridges.Telegram.Requests;
using Falko.Talkie.Bridges.Telegram.Responses;
using Falko.Talkie.Sequences;
using ModelsJsonSerializerContext = Talkie.Bridges.Telegram.Serialization.ModelsJsonSerializerContext;

namespace Falko.Talkie.Bridges.Telegram.Clients;

public sealed class TelegramClient : ITelegramClient
{
    private const string DownloadMethodName = "download";

    private const string Gzip = "gzip";

    private const string Utf8 = "utf-8";

    private const string UnknownName = "unknown";

    private const string ApplicationJson = "application/json";

    private readonly HttpClient _client;

    private readonly TelegramConfiguration _configuration;

    private readonly CancellationTokenSource _globalCancellationTokenSource = new();

    public TelegramClient(TelegramConfiguration configuration)
    {
        configuration.ThrowIfInvalid();

        _configuration = configuration;
        _client = BuildHttpClient(configuration);
    }

    public async Task<TResponse> SendRequestAsync<TResponse, TRequest>
    (
        string methodName,
        TRequest request,
        ITelegramRetryPolicy? policy = null,
        CancellationToken cancellationToken = default
    ) where TRequest : ITelegramRequest<TResponse> where TResponse : notnull
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentException.ThrowIfNullOrWhiteSpace(methodName);

        using var cancellationTokenSource = CreateGlobalLinkedTokenSource(cancellationToken);

        using var httpRequestContent = await BuildRequestContentAsync
        (
            methodName,
            request,
            cancellationTokenSource.Token
        );

        return await SendRepeatableRequestAsync<TResponse>
        (
            methodName: methodName,
            httpRequestContent: httpRequestContent,
            policy: policy,
            cancellationToken: cancellationTokenSource.Token
        );
    }

    public async Task<TResponse> SendRequestAsync<TResponse, TRequest>
    (
        string methodName,
        TRequest request,
        FrozenSequence<TelegramStream> streams,
        ITelegramRetryPolicy? policy = null,
        CancellationToken cancellationToken = default
    ) where TRequest : ITelegramRequest<TResponse> where TResponse : notnull
    {
        ArgumentNullException.ThrowIfNull(streams);
        ArgumentException.ThrowIfNullOrWhiteSpace(methodName);

        using var cancellationTokenSource = CreateGlobalLinkedTokenSource(cancellationToken);

        if (streams.Count is not 0)
        {
            using var httpRequestContent = BuildRequestContent
            (
                methodName: methodName,
                request: request,
                streams: streams,
                cancellationToken: cancellationTokenSource.Token
            );

            return await SendRepeatableRequestAsync<TResponse>
            (
                methodName: methodName,
                httpRequestContent: httpRequestContent,
                policy: policy,
                cancellationToken: cancellationTokenSource.Token
            );
        }
        else
        {
            using var httpRequestContent = await BuildRequestContentAsync
            (
                methodName,
                request,
                cancellationTokenSource.Token
            );

            return await SendRepeatableRequestAsync<TResponse>
            (
                methodName: methodName,
                httpRequestContent: httpRequestContent,
                policy: policy,
                cancellationToken: cancellationTokenSource.Token
            );
        }
    }

    public Task<TResult> SendRequestAsync<TResult>
    (
        string methodName,
        ITelegramRetryPolicy? policy = null,
        CancellationToken cancellationToken = default
    ) where TResult : notnull
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(methodName);

        using var cancellationTokenSource = CreateGlobalLinkedTokenSource(cancellationToken);

        return SendRepeatableRequestAsync<TResult>
        (
            methodName: methodName,
            httpRequestContent: null,
            policy: policy,
            cancellationToken: cancellationTokenSource.Token
        );
    }

    public async Task<Stream> DownloadRequestAsync
    (
        string file,
        ITelegramRetryPolicy? policy = null,
        CancellationToken cancellationToken = default
    )
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(file);

        var serverConfiguration = _configuration.ServerConfiguration;

        var fileHttpUri = new Uri($"https://{serverConfiguration.Domain}/file/bot{serverConfiguration.Token}/{file}");

        if (policy is null)
        {
            return await DownloadCoreAsync(fileHttpUri, cancellationToken);
        }

        while (true)
        {
            try
            {
                return await DownloadCoreAsync(fileHttpUri, cancellationToken);
            }
            catch (TelegramException exception)
            {
                try
                {
                    if (await policy.EvaluateAsync(exception, cancellationToken) is not true)
                    {
                        throw;
                    }
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (TelegramException)
                {
                    throw;
                }
                catch (Exception policyException)
                {
                    throw new TelegramException
                    (
                        client: this,
                        methodName: DownloadMethodName,
                        description: "Unknown error occurred while evaluating policy",
                        innerException: new AggregateException(policyException, exception)
                    );
                }
            }
        }
    }

    private async Task<Stream> DownloadCoreAsync(Uri fileHttpUri, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        try
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, fileHttpUri);

            var httpResponse = await _client.SendAsync(httpRequest, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

            if (httpResponse.IsSuccessStatusCode)
            {
                return await httpResponse.Content.ReadAsStreamAsync(cancellationToken);
            }

            var jsonResponse = await httpResponse.Content.ReadAsStringAsync(cancellationToken);

            var responseObject = JsonSerializer.Deserialize
            (
                jsonResponse,
                typeof(TelegramResponse),
                ModelsJsonSerializerContext.Default
            );

            if (responseObject is not TelegramResponse response)
            {
                throw new TelegramException
                (
                    client: this,
                    methodName: DownloadMethodName,
                    description: "Failed to deserialize response"
                );
            }

            throw new TelegramException
            (
                client: this,
                methodName: DownloadMethodName,
                statusCode: (HttpStatusCode?)response.ErrorCode,
                description: response.Description,
                parameters: response.Parameters
            );
        }
        catch (TelegramException)
        {
            throw;
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception exception)
        {
            throw new TelegramException
            (
                client: this,
                methodName: "download",
                description: "Unknown error occurred while sending request",
                innerException: exception
            );
        }
    }

    private async Task<TResponse> SendRepeatableRequestAsync<TResponse>
    (
        string methodName,
        HttpContent? httpRequestContent,
        ITelegramRetryPolicy? policy,
        CancellationToken cancellationToken
    ) where TResponse : notnull
    {
        if (policy is null)
        {
            var httpRequest = BuildRequest(methodName, httpRequestContent);

            return await SendCoreRequestAsync<TResponse>(methodName, httpRequest, cancellationToken);
        }

        while (true)
        {
            try
            {
                var httpRequest = BuildRequest(methodName, httpRequestContent);

                return await SendCoreRequestAsync<TResponse>(methodName, httpRequest, cancellationToken);
            }
            catch (TelegramException exception)
            {
                try
                {
                    if (await policy.EvaluateAsync(exception, cancellationToken) is not true)
                    {
                        throw;
                    }
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (TelegramException)
                {
                    throw;
                }
                catch (Exception policyException)
                {
                    throw new TelegramException
                    (
                        client: this,
                        methodName: methodName,
                        description: "Unknown error occurred while evaluating policy",
                        innerException: new AggregateException(policyException, exception)
                    );
                }
            }
        }
    }

    private async Task<TResponse> SendCoreRequestAsync<TResponse>
    (
        string methodName,
        HttpRequestMessage httpRequest,
        CancellationToken cancellationToken
    ) where TResponse : notnull
    {
        cancellationToken.ThrowIfCancellationRequested();

        try
        {
            using var httpResponse = await _client.SendAsync(httpRequest, cancellationToken);

            return await ParseHttpResponseAsync<TResponse>(methodName, httpResponse, cancellationToken);
        }
        catch (TelegramException)
        {
            throw;
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception exception)
        {
            throw new TelegramException
            (
                client: this,
                methodName: methodName,
                description: "Unknown error occurred while sending request",
                innerException: exception
            );
        }
    }

    private async Task<TResponse> ParseHttpResponseAsync<TResponse>
    (
        string methodName,
        HttpResponseMessage httpResponse,
        CancellationToken cancellationToken
    )
    {
        var responseJson = await httpResponse.Content.ReadAsStringAsync(cancellationToken);

        var responseObject = JsonSerializer.Deserialize
        (
            responseJson,
            typeof(TelegramResponse<TResponse>),
            ModelsJsonSerializerContext.Default
        );

        if (responseObject is not TelegramResponse<TResponse> response)
        {
            throw new TelegramException
            (
                client: this,
                methodName: methodName,
                description: "Failed to deserialize response"
            );
        }

        if (response.Ok is false || httpResponse.IsSuccessStatusCode is false)
        {
            throw new TelegramException
            (
                client: this,
                methodName: methodName,
                statusCode: (HttpStatusCode?)response.ErrorCode,
                description: response.Description,
                parameters: response.Parameters
            );
        }

        var result = response.Result;

        if (result is null)
        {
            throw new TelegramException
            (
                client: this,
                methodName: methodName,
                description: "Result is null"
            );
        }

        return result;
    }

    private HttpContent BuildRequestContent<TRequest>
    (
        string methodName,
        TRequest request,
        FrozenSequence<TelegramStream> streams,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var content = new MultipartFormDataContent();

            foreach (var stream in streams)
            {
                cancellationToken.ThrowIfCancellationRequested();

                ThrowIfStreamIsUnreadable(stream, methodName);

                content.Add(new StreamContent(stream.Stream), stream.Identifier.ToString(), stream.Name ?? UnknownName);
            }

            var requestJson = JsonSerializer.SerializeToDocument
            (
                request,
                typeof(TRequest),
                ModelsJsonSerializerContext.Default
            );

            foreach (var requestJsonPart in requestJson.RootElement.EnumerateObject())
            {
                cancellationToken.ThrowIfCancellationRequested();

                content.Add(new StringContent(requestJsonPart.Value.ToString(), Encoding.UTF8), requestJsonPart.Name);
            }

            return content;
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception exception)
        {
            throw new TelegramException
            (
                client: this,
                methodName: methodName,
                description: "Unknown error occurred while building request",
                innerException: exception
            );
        }
    }

    private void ThrowIfStreamIsUnreadable(TelegramStream stream, string methodName)
    {
        if (stream.Stream.CanRead) return;

        throw new TelegramException
        (
            client: this,
            methodName: methodName,
            description: $"Stream with name '{stream.Name}' is not readable"
        );
    }

    private async Task<HttpContent> BuildRequestContentAsync<TRequest>
    (
        string methodName,
        TRequest request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var requestJson = JsonSerializer.Serialize
            (
                request,
                typeof(TRequest),
                ModelsJsonSerializerContext.Default
            );

            if (_configuration.ClientConfiguration.UseGzipCompression)
            {
                return await BuildGzipJsonContentAsync(requestJson, cancellationToken);
            }

            return BuildJsonContent(requestJson);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception exception)
        {
            throw new TelegramException
            (
                client: this,
                methodName: methodName,
                description: "Unknown error occurred while building request",
                innerException: exception
            );
        }
    }

    private static StringContent BuildJsonContent(string requestJson)
    {
        return new StringContent(requestJson, Encoding.UTF8, ApplicationJson);
    }

    private static HttpRequestMessage BuildRequest(string methodName, HttpContent? httpRequestContent = null)
    {
        return new HttpRequestMessage(HttpMethod.Post, methodName)
        {
            Content = httpRequestContent
        };
    }

    private async Task<HttpContent> BuildGzipJsonContentAsync
    (
        string requestJson,
        CancellationToken cancellationToken
    )
    {
        await using var memoryStream = new MemoryStream();

        await using var gzipStream = new GZipStream
        (
            memoryStream,
            _configuration.ClientConfiguration.GzipCompressionLevel
        );

        var requestBytes = Encoding.UTF8.GetBytes(requestJson);

        await gzipStream.WriteAsync(requestBytes, cancellationToken);

        gzipStream.Close();
        memoryStream.Close();

        var content = new ByteArrayContent(memoryStream.ToArray());
        content.Headers.ContentType = new MediaTypeHeaderValue(ApplicationJson);
        content.Headers.ContentEncoding.Add(Gzip);

        return content;
    }

    private CancellationTokenSource CreateGlobalLinkedTokenSource(CancellationToken cancellationToken)
    {
        return CancellationTokenSource.CreateLinkedTokenSource
        (
            _globalCancellationTokenSource.Token,
            cancellationToken
        );
    }

    public void Dispose()
    {
        _globalCancellationTokenSource.Cancel();
        _globalCancellationTokenSource.Dispose();

        _client.Dispose();
    }

    private static HttpClient BuildHttpClient(TelegramConfiguration configuration)
    {
        var clientConfiguration = configuration.ClientConfiguration;
        var serverConfiguration = configuration.ServerConfiguration;

        var client = new HttpClient(new SocketsHttpHandler
        {
            EnableMultipleHttp2Connections = true,
            AutomaticDecompression = clientConfiguration.UseGzipDecompression
                ? DecompressionMethods.GZip
                : DecompressionMethods.None,
            MaxConnectionsPerServer = clientConfiguration.ConnectionsPoolMaxSize,
            PooledConnectionIdleTimeout = clientConfiguration.PooledConnectionIdleTimeout,
            PooledConnectionLifetime = clientConfiguration.PooledConnectionLifetime,
            ConnectTimeout = clientConfiguration.ConnectTimeout,
            KeepAlivePingDelay = clientConfiguration.KeepAlivePingDelay,
            KeepAlivePingTimeout = clientConfiguration.KeepAlivePingTimeout,
            UseCookies = false,
            UseProxy = false
        })
        {
            DefaultRequestVersion = clientConfiguration.ProtocolVersion switch
            {
                HttpProtocol.Version10 => HttpVersion.Version10,
                HttpProtocol.Version11 => HttpVersion.Version11,
                HttpProtocol.Version20 => HttpVersion.Version20,
                _ => throw new ArgumentOutOfRangeException(nameof(clientConfiguration.ProtocolVersion))
            },
            BaseAddress = new Uri($"https://{serverConfiguration.Domain}/bot{serverConfiguration.Token}/")
        };

        client.DefaultRequestHeaders.UserAgent.ParseAdd(nameof(global::Talkie));

        if (clientConfiguration.UseGzipCompression)
        {
            client.DefaultRequestHeaders.AcceptEncoding.ParseAdd(Gzip);
        }

        client.DefaultRequestHeaders.AcceptCharset.ParseAdd(Utf8);
        client.DefaultRequestHeaders.Accept.ParseAdd(ApplicationJson);

        client.DefaultVersionPolicy = HttpVersionPolicy.RequestVersionExact;

        return client;
    }
}
