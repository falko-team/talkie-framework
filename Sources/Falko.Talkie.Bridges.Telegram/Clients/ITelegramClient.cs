using Talkie.Bridges.Telegram.Models;
using Talkie.Bridges.Telegram.Requests;
using Talkie.Sequences;

namespace Talkie.Bridges.Telegram.Clients;

public interface ITelegramClient : IDisposable
{
    Task<TResult> SendAsync<TResult, TRequest>
    (
        string methodName,
        TRequest request,
        CancellationToken cancellationToken = default
    ) where TRequest : ITelegramRequest<TResult> where TResult : notnull;

    Task<TResult> SendAsync<TResult>
    (
        string methodName,
        CancellationToken cancellationToken = default
    ) where TResult : notnull;

    Task<TResult> SendAsync<TResult>
    (
        string methodName,
        FrozenSequence<TelegramStream> stream,
        CancellationToken cancellationToken = default
    ) where TResult : notnull;

    Task<Stream> DownloadAsync
    (
        string file,
        CancellationToken cancellationToken = default
    );
}
