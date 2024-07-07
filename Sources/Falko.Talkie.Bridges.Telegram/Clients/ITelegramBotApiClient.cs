namespace Talkie.Bridges.Telegram.Clients;

public interface ITelegramBotApiClient : IDisposable
{
    internal Task<TResult> SendAsync<TResult, TRequest>(string methodName, TRequest request,
        CancellationToken cancellationToken = default) where TResult : class where TRequest : class;

    internal Task<TResult> SendAsync<TResult>(string methodName,
        CancellationToken cancellationToken = default) where TResult : class;

    internal Task SendAsync<TRequest>(string methodName, TRequest request,
        CancellationToken cancellationToken = default) where TRequest : class;

    internal Task SendAsync(string methodName,
        CancellationToken cancellationToken = default);
}
