using System.Net;
using Talkie.Bridges.Telegram.Exceptions;

namespace Talkie.Bridges.Telegram.Policies;

public sealed class TelegramTooManyRequestGlobalRetryPolicy(TimeSpan defaultDelay = default) : ITelegramRetryPolicy
{
    private const int IsDelaying = 1;

    private const int IsNotDelaying = 0;

    private readonly TimeSpan _defaultDelay = defaultDelay == TimeSpan.Zero
        ? TimeSpan.FromSeconds(3)
        : defaultDelay;

    private TaskCompletionSource<bool> _delaySource = new();

    private int _delayingState = IsNotDelaying;

    public async ValueTask<bool> EvaluateAsync(TelegramException exception, CancellationToken cancellationToken)
    {
        if (exception.StatusCode is not HttpStatusCode.TooManyRequests)
        {
            return false;
        }

        cancellationToken.ThrowIfCancellationRequested();

        if (Interlocked.CompareExchange(ref _delayingState, IsDelaying, IsNotDelaying) is IsNotDelaying)
        {
            try
            {
                _delaySource = new TaskCompletionSource<bool>();

                var currentDelay = _defaultDelay;

                if (exception.Parameters.TryGetValue(TelegramException.ParameterNames.RetryAfter, out var delay)
                    && delay.TryGetNumber(out var delaySeconds))
                {
                    currentDelay = TimeSpan.FromSeconds(delaySeconds);

                    if (currentDelay < _defaultDelay) currentDelay = _defaultDelay;
                }

                await Task.Delay(currentDelay, cancellationToken);
            }
            finally
            {
                _delaySource.SetResult(true);
                Interlocked.Exchange(ref _delayingState, IsNotDelaying);
            }

            return true;
        }

        await _delaySource.Task;

        return true;
    }
}
