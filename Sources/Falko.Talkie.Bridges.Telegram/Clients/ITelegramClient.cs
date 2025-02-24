using Talkie.Bridges.Telegram.Models;
using Talkie.Bridges.Telegram.Requests;
using Talkie.Sequences;

namespace Talkie.Bridges.Telegram.Clients;

public interface ITelegramClient : IDisposable
{
    Task<TResult> SendRequestAsync<TResult, TRequest>
    (
        string methodName,
        TRequest request,
        CancellationToken cancellationToken = default
    ) where TRequest : ITelegramRequest<TResult> where TResult : notnull;

    Task<TResult> SendRequestAsync<TResult, TRequest>
    (
        string methodName,
        TRequest request,
        FrozenSequence<TelegramStream> streams,
        CancellationToken cancellationToken = default
    ) where TRequest : ITelegramRequest<TResult> where TResult : notnull;

    Task<TResult> SendRequestAsync<TResult>
    (
        string methodName,
        CancellationToken cancellationToken = default
    ) where TResult : notnull;

    Task<Stream> DownloadRequestAsync
    (
        string file,
        CancellationToken cancellationToken = default
    );
}
