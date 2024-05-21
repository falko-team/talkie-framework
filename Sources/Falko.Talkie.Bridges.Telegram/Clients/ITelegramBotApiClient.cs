namespace Falko.Talkie.Bridges.Telegram.Clients;

public interface ITelegramBotApiClient : IDisposable
{
    Task<TResult> SendAsync<TResult, TRequest>(string methodName, TRequest request,
        CancellationToken cancellationToken = default) where TResult : class where TRequest : class;

    Task<TResult> SendAsync<TResult>(string method, CancellationToken cancellationToken = default)
        where TResult : class;

    Task SendAsync<TRequest>(string method, TRequest request, CancellationToken cancellationToken = default)
        where TRequest : class;

    Task SendAsync(string method, CancellationToken cancellationToken = default);
}
