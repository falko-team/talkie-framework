using System.IO.Compression;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Talkie.Bridges.Telegram.Configurations;
using Talkie.Bridges.Telegram.Models;
using Talkie.Bridges.Telegram.Requests;
using Talkie.Bridges.Telegram.Responses;
using Talkie.Bridges.Telegram.Serialization;
using Talkie.Sequences;

namespace Talkie.Bridges.Telegram.Clients;

public sealed class TelegramClient : ITelegramClient
{
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

    public Task<TResponse> SendRequestAsync<TResponse, TRequest>
    (
        string methodName,
        TRequest request,
        CancellationToken cancellationToken
    ) where TRequest : ITelegramRequest<TResponse> where TResponse : notnull
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentException.ThrowIfNullOrWhiteSpace(methodName);

        using var cancellationTokenSource = CreateGlobalLinkedTokenSource(cancellationToken);

        return SendRepeatableRequestAsync<TResponse, TRequest>
        (
            methodName: methodName,
            request: request,
            cancellationToken: cancellationTokenSource.Token
        );
    }

    public Task<TResponse> SendRequestAsync<TResponse, TRequest>
    (
        string methodName,
        TRequest request,
        FrozenSequence<TelegramStream> streams,
        CancellationToken cancellationToken = default
    ) where TRequest : ITelegramRequest<TResponse> where TResponse : notnull
    {
        ArgumentNullException.ThrowIfNull(streams);
        ArgumentException.ThrowIfNullOrWhiteSpace(methodName);

        if (streams.Count is 0)
        {
            return SendRequestAsync<TResponse, TRequest>
            (
                methodName: methodName,
                request: request,
                cancellationToken: cancellationToken
            );
        }

        using var cancellationTokenSource = CreateGlobalLinkedTokenSource(cancellationToken);

        return SendRepeatableRequestAsync<TResponse, TRequest>
        (
            methodName: methodName,
            request: request,
            streams: streams,
            cancellationToken: cancellationTokenSource.Token
        );
    }

    public Task<TResult> SendRequestAsync<TResult>
    (
        string methodName,
        CancellationToken cancellationToken = default
    ) where TResult : notnull
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(methodName);

        using var cancellationTokenSource = CreateGlobalLinkedTokenSource(cancellationToken);

        return SendRepeatableRequestAsync<TResult, TelegramRequest<TResult>>
        (
            methodName: methodName,
            request: null,
            cancellationToken: cancellationTokenSource.Token
        );
    }

    public async Task<Stream> DownloadRequestAsync
    (
        string file,
        CancellationToken cancellationToken
    )
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(file);

        while (true)
        {
            try
            {
                return await DownloadCoreAsync(file, cancellationToken);
            }
            catch (TelegramException exception)
            {
                if (exception.StatusCode is null or not HttpStatusCode.TooManyRequests)
                {
                    throw;
                }

                await WaitRetryDelayAsync(exception, cancellationToken);
            }
        }
    }

    private async Task<TResponse> SendCoreAsync<TResponse>
    (
        string methodName,
        HttpRequestMessage request,
        CancellationToken cancellationToken = default
    ) where TResponse : notnull
    {
        try
        {
            using var httpResponse = await _client.SendAsync(request, cancellationToken);

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

    private async Task<Stream> DownloadCoreAsync(string file, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        try
        {
            var serverConfiguration = _configuration.ServerConfiguration;

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, file)
            {
                RequestUri = new Uri($"https://{serverConfiguration.Domain}/file/bot{serverConfiguration.Token}/{file}")
            };

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
                    methodName: "download",
                    description: "Failed to deserialize response"
                );
            }

            throw new TelegramException
            (
                client: this,
                methodName: "download",
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

    private async Task<TResponse> SendRepeatableRequestAsync<TResponse, TRequest>
    (
        string methodName,
        TRequest request,
        FrozenSequence<TelegramStream> streams,
        CancellationToken cancellationToken
    ) where TRequest : ITelegramRequest<TResponse> where TResponse : notnull
    {
        using var httpRequest = BuildHttpRequest(methodName, request, streams, cancellationToken);

        while (true)
        {
            try
            {
                return await SendCoreAsync<TResponse>(methodName, httpRequest, cancellationToken);
            }
            catch (TelegramException exception)
            {
                if (exception.StatusCode is null or not HttpStatusCode.TooManyRequests)
                {
                    throw;
                }

                await WaitRetryDelayAsync(exception, cancellationToken);
            }
        }
    }

    private async Task<TResponse> SendRepeatableRequestAsync<TResponse, TRequest>
    (
        string methodName,
        TRequest? request,
        CancellationToken cancellationToken
    ) where TRequest : ITelegramRequest<TResponse> where TResponse : notnull
    {
        using var httpRequest = await BuildHttpRequestAsync(methodName, request, cancellationToken);

        while (true)
        {
            try
            {
                return await SendCoreRequestAsync<TResponse>(methodName, httpRequest, cancellationToken);
            }
            catch (TelegramException exception)
            {
                if (exception.StatusCode is null or not HttpStatusCode.TooManyRequests)
                {
                    throw;
                }

                await WaitRetryDelayAsync(exception, cancellationToken);
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

    private HttpRequestMessage BuildHttpRequest<TRequest>
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

                var internalStream = stream.Stream;

                if (internalStream.CanRead is false)
                {
                    throw new TelegramException
                    (
                        client: this,
                        methodName: methodName,
                        description: $"Stream with name '{stream.Name}' is not readable"
                    );
                }

                if (internalStream.Position is not 0)
                {
                    if (internalStream.CanSeek)
                    {
                        internalStream.Position = 0;
                    }
                    else
                    {
                        throw new TelegramException
                        (
                            client: this,
                            methodName: methodName,
                            description: $"Stream with name '{stream.Name}' is not seekable and its position is not 0"
                        );
                    }
                }

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

            return new HttpRequestMessage(HttpMethod.Post, methodName)
            {
                Content = content
            };
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

    private async Task<HttpRequestMessage> BuildHttpRequestAsync<TRequest>
    (
        string methodName,
        TRequest? request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, methodName);

            if (request is null) return httpRequest;

            var requestJson = JsonSerializer.Serialize
            (
                request,
                typeof(TRequest),
                ModelsJsonSerializerContext.Default
            );

            if (_configuration.ClientConfiguration.UseGzipCompression)
            {
                await AddGzipJsonContentAsync(httpRequest, requestJson, cancellationToken);
            }
            else
            {
                AddJsonContent(httpRequest, requestJson);
            }

            return httpRequest;
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

    private static void AddJsonContent(HttpRequestMessage httpRequest, string requestJson)
    {
        httpRequest.Content = new StringContent(requestJson, Encoding.UTF8, ApplicationJson);
    }

    private async Task AddGzipJsonContentAsync
    (
        HttpRequestMessage httpRequest,
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

        httpRequest.Content = new ByteArrayContent(memoryStream.ToArray());
        httpRequest.Content.Headers.ContentType = new MediaTypeHeaderValue(ApplicationJson);
        httpRequest.Content.Headers.ContentEncoding.Add(Gzip);
    }

    private async Task WaitRetryDelayAsync(TelegramException exception, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (exception.Parameters.TryGetValue(TelegramException.ParameterNames.RetryAfter, out var delay)
            && delay.TryGetNumber(out var delaySeconds))
        {
            await Task.Delay(TimeSpan.FromSeconds(delaySeconds), cancellationToken);
        }
        else
        {
            await Task.Delay(_configuration.ServerConfiguration.DefaultRetryDelay, cancellationToken);
        }
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

        client.DefaultRequestHeaders.UserAgent.ParseAdd(nameof(Talkie));

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
