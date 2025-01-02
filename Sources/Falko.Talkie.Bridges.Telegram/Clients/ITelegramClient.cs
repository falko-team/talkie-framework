using Talkie.Bridges.Telegram.Requests;

namespace Talkie.Bridges.Telegram.Clients;

public interface ITelegramClient : IDisposable
{
    internal Task<TResult> SendAsync<TResult, TRequest>
    (
        string methodName,
        TRequest request,
        CancellationToken cancellationToken = default
    ) where TRequest : ITelegramRequest<TResult> where TResult : notnull;

    internal Task<TResult> SendAsync<TResult>(string methodName, CancellationToken cancellationToken = default)
        where TResult : notnull;

    internal Task SendAsync<TRequest>
    (
        string methodName,
        TRequest request,
        CancellationToken cancellationToken = default
    );

    internal Task SendAsync(string methodName, CancellationToken cancellationToken = default);

    Task<Stream> DownloadAsync(string file, CancellationToken cancellationToken = default);
}
