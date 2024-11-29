using System.IO.Compression;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Talkie.Bridges.Telegram.Configurations;
using Talkie.Bridges.Telegram.Models;
using Talkie.Bridges.Telegram.Responses;
using Talkie.Bridges.Telegram.Serialization;

namespace Talkie.Bridges.Telegram.Clients;

public sealed class TelegramClient : ITelegramClient
{
    private const string Gzip = "gzip";

    private const string Utf8 = "utf-8";

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

    async Task<TResult> ITelegramClient.SendAsync<TResult, TRequest>
    (
        string methodName,
        TRequest request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentException.ThrowIfNullOrWhiteSpace(methodName);

        using var scopedCancellationTokenSource = CancellationTokenSource
            .CreateLinkedTokenSource(_globalCancellationTokenSource.Token, cancellationToken);

        return await SendRepeatableRequestAsync<TResult, TRequest>(methodName, request, scopedCancellationTokenSource.Token)
            ?? throw new TelegramException(this, methodName,
                description: "Result is null");
    }

    async Task<TResult> ITelegramClient.SendAsync<TResult>(string methodName, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(methodName);

        using var scopedCancellationTokenSource = CancellationTokenSource
            .CreateLinkedTokenSource(_globalCancellationTokenSource.Token, cancellationToken);

        var result = await SendRepeatableRequestAsync<TResult, object>(methodName, null, scopedCancellationTokenSource.Token);

        ArgumentNullException.ThrowIfNull(result);

        return result;
    }

    Task ITelegramClient.SendAsync<TRequest>(string methodName, TRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentException.ThrowIfNullOrWhiteSpace(methodName);

        using var scopedCancellationTokenSource = CancellationTokenSource
            .CreateLinkedTokenSource(_globalCancellationTokenSource.Token, cancellationToken);

        return SendRepeatableRequestAsync<object, TRequest>(methodName, request, scopedCancellationTokenSource.Token);
    }

    Task ITelegramClient.SendAsync(string methodName, CancellationToken cancellationToken)
    {
        using var scopedCancellationTokenSource = CancellationTokenSource
            .CreateLinkedTokenSource(_globalCancellationTokenSource.Token, cancellationToken);

        return SendRepeatableRequestAsync<object, object>(methodName, null, scopedCancellationTokenSource.Token);
    }

    public async Task<Stream> DownloadAsync(string file, CancellationToken cancellationToken)
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

            Console.WriteLine(httpRequest.RequestUri);

            var httpResponse = await _client.SendAsync(httpRequest, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

            if (httpResponse.IsSuccessStatusCode)
            {
                return await httpResponse.Content.ReadAsStreamAsync(cancellationToken);
            }

            var jsonResponse = await httpResponse.Content.ReadAsStringAsync(cancellationToken);

            if (JsonSerializer.Deserialize(jsonResponse, typeof(TelegramResponse), ModelsJsonSerializerContext.Default)
                is not TelegramResponse response)
            {
                throw new TelegramException(this, "download",
                    description: "Failed to deserialize response");
            }

            throw new TelegramException(this, "download",
                statusCode: (HttpStatusCode?)response.ErrorCode,
                description: response.Description,
                parameters: response.Parameters);
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
            throw new TelegramException(this, "download",
                description: "Unknown error occurred while sending request",
                innerException: exception);
        }
    }

    private async Task<TResult?> SendRepeatableRequestAsync<TResult, TRequest>
    (
        string method,
        TRequest? request,
        CancellationToken cancellationToken
    )
    {
        while (true)
        {
            try
            {
                return await SendRequestAsync<TResult, TRequest>(method, request, cancellationToken);
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

    private async Task<TResult?> SendRequestAsync<TResult, TRequest>
    (
        string method,
        TRequest? request,
        CancellationToken cancellationToken
    )
    {
        cancellationToken.ThrowIfCancellationRequested();

        try
        {
            using var httpRequest = await BuildHttpRequestAsync(method, request, cancellationToken);

            using var httpResponse = await _client.SendAsync(httpRequest, cancellationToken);

            return await ParseHttpResponseAsync<TResult>(method, httpResponse, cancellationToken);
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
            throw new TelegramException(this, method,
                description: "Unknown error occurred while sending request",
                innerException: exception);
        }
    }

    private async Task<TResult?> ParseHttpResponseAsync<TResult>
    (
        string method,
        HttpResponseMessage httpResponse,
        CancellationToken cancellationToken
    )
    {
        if (typeof(TResult) == typeof(object))
        {
            return default;
        }

        var responseJson = await httpResponse.Content.ReadAsStringAsync(cancellationToken);

        if (JsonSerializer.Deserialize(responseJson, typeof(TelegramResponse<TResult>), ModelsJsonSerializerContext.Default)
            is not TelegramResponse<TResult> response)
        {
            throw new TelegramException(this, method,
                description: "Failed to deserialize response");
        }

        if (response.Ok is false || httpResponse.IsSuccessStatusCode is false)
        {
            throw new TelegramException(this, method,
                statusCode: (HttpStatusCode?)response.ErrorCode,
                description: response.Description,
                parameters: response.Parameters);
        }

        return response.Result;
    }

    private async Task<HttpRequestMessage> BuildHttpRequestAsync<TRequest>
    (
        string method,
        TRequest? request,
        CancellationToken cancellationToken
    )
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, method);

        if (request is null) return httpRequest;

        var requestJson = JsonSerializer.Serialize(request, typeof(TRequest), ModelsJsonSerializerContext.Default);

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

    private static void AddJsonContent(HttpRequestMessage httpRequest, string requestJson)
    {
        httpRequest.Content = new StringContent(requestJson, Encoding.UTF8, ApplicationJson);
    }

    private static async Task AddGzipJsonContentAsync
    (
        HttpRequestMessage httpRequest,
        string requestJson,
        CancellationToken cancellationToken
    )
    {
        await using var memoryStream = new MemoryStream();
        await using var gzipStream = new GZipStream(memoryStream, CompressionMode.Compress);

        var requestBytes = Encoding.UTF8.GetBytes(requestJson);

        await gzipStream.WriteAsync(requestBytes, 0, requestBytes.Length, cancellationToken);

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
