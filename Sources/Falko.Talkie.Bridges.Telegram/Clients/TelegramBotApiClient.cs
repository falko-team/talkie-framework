using System.Net;
using System.Text;
using System.Text.Json;
using Talkie.Bridges.Telegram.Models;
using Talkie.Bridges.Telegram.Serialization;
using Talkie.Validations;

namespace Talkie.Bridges.Telegram.Clients;

public sealed class TelegramBotApiClient(string token) : ITelegramBotApiClient
{
    private readonly HttpClient _client = BuildClient(token);

    private readonly CancellationTokenSource _globalCancellationTokenSource = new();

    public async Task<TResult> SendAsync<TResult, TRequest>(string method, TRequest request,
        CancellationToken cancellationToken = default) where TResult : class where TRequest : class
    {
        method.ThrowIf().NullOrWhiteSpace();

        using var scopedCancellationTokenSource = CancellationTokenSource
            .CreateLinkedTokenSource(_globalCancellationTokenSource.Token, cancellationToken);

        return await SendRepeatableRequestAsync<TResult, TRequest>(method, request, scopedCancellationTokenSource.Token)
            ?? throw new TelegramBotApiRequestException(method, "Result is null");
    }

    public async Task<TResult> SendAsync<TResult>(string method, CancellationToken cancellationToken = default)
        where TResult : class
    {
        using var scopedCancellationTokenSource = CancellationTokenSource
            .CreateLinkedTokenSource(_globalCancellationTokenSource.Token, cancellationToken);

        return await SendRepeatableRequestAsync<TResult, object>(method, null, scopedCancellationTokenSource.Token)
            ?? throw new TelegramBotApiRequestException(method, "Result is null");
    }

    public Task SendAsync<TRequest>(string method, TRequest request, CancellationToken cancellationToken = default)
        where TRequest : class
    {
        using var scopedCancellationTokenSource = CancellationTokenSource
            .CreateLinkedTokenSource(_globalCancellationTokenSource.Token, cancellationToken);

        return SendRepeatableRequestAsync<object, TRequest>(method, request, scopedCancellationTokenSource.Token);
    }

    public Task SendAsync(string method, CancellationToken cancellationToken = default)
    {
        using var scopedCancellationTokenSource = CancellationTokenSource
            .CreateLinkedTokenSource(_globalCancellationTokenSource.Token, cancellationToken);

        return SendRepeatableRequestAsync<object, object>(method, null, scopedCancellationTokenSource.Token);
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
                if (exception.Code is not 429)
                {
                    throw;
                }

                try
                {
                    if (int.TryParse(exception.Parameters?["retry_after"], out var seconds))
                    {
                        await Task.Delay(TimeSpan.FromSeconds(seconds), cancellationToken);
                    }
                    else
                    {
                        await Task.Delay(TimeSpan.FromSeconds(30), cancellationToken);
                    }
                }
                catch
                {
                    throw new OperationCanceledException();
                }
            }
        }
    }

    private async Task<TResult?> SendRequestAsync<TResult, TRequest>(string method, TRequest? request,
        CancellationToken cancellationToken = default) where TResult : class where TRequest : class
    {
        cancellationToken.ThrowIfCancellationRequested();

        try
        {
            using var httpRequest = new HttpRequestMessage(HttpMethod.Post, method);

            if (request is not null)
            {
                var requestJson = JsonSerializer.Serialize(request, typeof(TRequest), ModelsJsonSerializerContext.Default);

                httpRequest.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");
            }

            using var httpResponse = await _client.SendAsync(httpRequest, cancellationToken);

            var responseJson = await httpResponse.Content.ReadAsStringAsync(cancellationToken);

            var response = JsonSerializer.Deserialize(responseJson, typeof(Response<TResult>),
                ModelsJsonSerializerContext.Default) as Response<TResult>;

            if (response is null)
            {
                throw new TelegramBotApiRequestException(method, "Failed to deserialize response");
            }

            if (response.Ok is false || httpResponse.IsSuccessStatusCode is false)
            {
                throw new TelegramBotApiRequestException(method, response.Description, response.ErrorCode);
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
            throw new TelegramBotApiRequestException(method, "Unknown error occurred while sending request",
                inner: exception);
        }
    }

    public void Dispose()
    {
        _globalCancellationTokenSource.Cancel();
        _globalCancellationTokenSource.Dispose();

        _client.Dispose();
    }

    private static HttpClient BuildClient(string token)
    {
        return new HttpClient(new SocketsHttpHandler
        {
            AutomaticDecompression = DecompressionMethods.Brotli | DecompressionMethods.GZip,
            MaxConnectionsPerServer = 100,
            PooledConnectionIdleTimeout = TimeSpan.FromSeconds(30),
            PooledConnectionLifetime = TimeSpan.FromMinutes(1),
            ConnectTimeout = TimeSpan.FromSeconds(30),
            KeepAlivePingDelay = TimeSpan.FromSeconds(60),
            KeepAlivePingTimeout = TimeSpan.FromSeconds(15),
            UseCookies = false,
            UseProxy = false
        })
        {
            BaseAddress = new Uri($"https://api.telegram.org/bot{token}/"),
            DefaultRequestHeaders =
            {
                { "Accept", "application/json" }
            }
        };
    }
}
