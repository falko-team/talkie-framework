using System.IO.Compression;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Talkie.Bridges.Telegram.Configurations;
using Talkie.Bridges.Telegram.Models;
using Talkie.Bridges.Telegram.Serialization;
using Talkie.Validations;
using ClientHttpVersion = System.Net.HttpVersion;

namespace Talkie.Bridges.Telegram.Clients;

public sealed class TelegramBotApiClient : ITelegramBotApiClient
{
    private const string Gzip = "gzip";

    private const string Utf8 = "utf-8";

    private const string ApplicationJson = "application/json";

    private readonly HttpClient _client;

    private readonly bool _useGzipCompression;

    private readonly TimeSpan _defaultRetryDelay;

    private readonly CancellationTokenSource _globalCancellationTokenSource = new();

    public TelegramBotApiClient(ServerConfiguration serverConfiguration,
        ClientConfiguration? clientConfiguration = null)
    {
        clientConfiguration ??= new ClientConfiguration();

        _useGzipCompression = clientConfiguration.UseGzipCompression;
        _defaultRetryDelay = serverConfiguration.DefaultRetryDelay;
        _client = BuildHttpClient(serverConfiguration, clientConfiguration);
    }

    async Task<TResult> ITelegramBotApiClient.SendAsync<TResult, TRequest>(string methodName, TRequest request,
        CancellationToken cancellationToken) where TResult : class where TRequest : class
    {
        methodName.ThrowIf().NullOrWhiteSpace();

        using var scopedCancellationTokenSource = CancellationTokenSource
            .CreateLinkedTokenSource(_globalCancellationTokenSource.Token, cancellationToken);

        return await SendRepeatableRequestAsync<TResult, TRequest>(methodName, request, scopedCancellationTokenSource.Token)
            ?? throw new TelegramBotApiRequestException(this, methodName,
                description: "Result is null");
    }

    async Task<TResult> ITelegramBotApiClient.SendAsync<TResult>(string methodName,
        CancellationToken cancellationToken) where TResult : class
    {
        using var scopedCancellationTokenSource = CancellationTokenSource
            .CreateLinkedTokenSource(_globalCancellationTokenSource.Token, cancellationToken);

        return await SendRepeatableRequestAsync<TResult, object>(methodName, null, scopedCancellationTokenSource.Token)
            ?? throw new TelegramBotApiRequestException(this, methodName,
                description: "Result is null");
    }

    Task ITelegramBotApiClient.SendAsync<TRequest>(string methodName, TRequest request,
        CancellationToken cancellationToken) where TRequest : class
    {
        using var scopedCancellationTokenSource = CancellationTokenSource
            .CreateLinkedTokenSource(_globalCancellationTokenSource.Token, cancellationToken);

        return SendRepeatableRequestAsync<object, TRequest>(methodName, request, scopedCancellationTokenSource.Token);
    }

    Task ITelegramBotApiClient.SendAsync(string methodName,
        CancellationToken cancellationToken)
    {
        using var scopedCancellationTokenSource = CancellationTokenSource
            .CreateLinkedTokenSource(_globalCancellationTokenSource.Token, cancellationToken);

        return SendRepeatableRequestAsync<object, object>(methodName, null, scopedCancellationTokenSource.Token);
    }

    private async Task<TResult?> SendRepeatableRequestAsync<TResult, TRequest>(string method, TRequest? request,
        CancellationToken cancellationToken) where TResult : class where TRequest : class
    {
        while (true)
        {
            try
            {
                return await SendRequestAsync<TResult, TRequest>(method, request, cancellationToken);
            }
            catch (TelegramBotApiRequestException exception)
            {
                if (exception.StatusCode is null or not HttpStatusCode.TooManyRequests)
                {
                    throw;
                }

                cancellationToken.ThrowIfCancellationRequested();

                if (exception.Parameters.TryGetValue(TelegramBotApiRequestException.Parameter.RetryAfter, out var delay)
                    && delay.TryGetNumber(out var delaySeconds))
                {
                    await Task.Delay(TimeSpan.FromSeconds(delaySeconds), cancellationToken);
                }
                else
                {
                    await Task.Delay(_defaultRetryDelay, cancellationToken);
                }
            }
        }
    }

    private async Task<TResult?> SendRequestAsync<TResult, TRequest>(string method, TRequest? request,
        CancellationToken cancellationToken) where TResult : class where TRequest : class
    {
        cancellationToken.ThrowIfCancellationRequested();

        try
        {
            using var httpRequest = new HttpRequestMessage(HttpMethod.Post, method);

            if (request is not null)
            {
                var requestJson = JsonSerializer.Serialize(request, typeof(TRequest), ModelsJsonSerializerContext.Default);

                if (_useGzipCompression)
                {
                    await AddGzipJsonContentAsync(httpRequest, requestJson, cancellationToken);
                }
                else
                {
                    AddJsonContent(httpRequest, requestJson);
                }
            }

            using var httpResponse = await _client.SendAsync(httpRequest, cancellationToken);

            var responseJson = await httpResponse.Content.ReadAsStringAsync(cancellationToken);

            if (JsonSerializer.Deserialize(responseJson, typeof(Response<TResult>), ModelsJsonSerializerContext.Default)
                is not Response<TResult> response)
            {
                throw new TelegramBotApiRequestException(this, method,
                    description: "Failed to deserialize response");
            }

            if (response.Ok is false || httpResponse.IsSuccessStatusCode is false)
            {
                throw new TelegramBotApiRequestException(this, method,
                    statusCode: (HttpStatusCode?)response.ErrorCode,
                    description: response.Description,
                    parameters: response.Parameters);
            }

            return response.Result;
        }
        catch (TelegramBotApiRequestException)
        {
            throw;
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception exception)
        {
            throw new TelegramBotApiRequestException(this, method,
                description: "Unknown error occurred while sending request",
                innerException: exception);
        }
    }

    private static void AddJsonContent(HttpRequestMessage httpRequest, string requestJson)
    {
        httpRequest.Content = new StringContent(requestJson, Encoding.UTF8, ApplicationJson);
    }

    private static async Task AddGzipJsonContentAsync(HttpRequestMessage httpRequest, string requestJson,
        CancellationToken cancellationToken)
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

    public void Dispose()
    {
        _globalCancellationTokenSource.Cancel();
        _globalCancellationTokenSource.Dispose();

        _client.Dispose();
    }

    private static HttpClient BuildHttpClient(ServerConfiguration serverConfiguration, ClientConfiguration clientConfiguration)
    {
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
                HttpProtocol.Version10 => ClientHttpVersion.Version10,
                HttpProtocol.Version11 => ClientHttpVersion.Version11,
                HttpProtocol.Version20 => ClientHttpVersion.Version20,
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
