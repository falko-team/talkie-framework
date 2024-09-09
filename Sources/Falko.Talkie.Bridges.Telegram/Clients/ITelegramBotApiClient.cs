namespace Talkie.Bridges.Telegram.Clients;

public interface ITelegramBotApiClient : IDisposable
{
    internal Task<TResult> SendAsync<TResult, TRequest>(string methodName, TRequest request,
        CancellationToken cancellationToken = default);

    internal Task<TResult> SendAsync<TResult>(string methodName,
        CancellationToken cancellationToken = default);

    internal Task SendAsync<TRequest>(string methodName, TRequest request,
        CancellationToken cancellationToken = default);

    internal Task SendAsync(string methodName,
        CancellationToken cancellationToken = default);
}
