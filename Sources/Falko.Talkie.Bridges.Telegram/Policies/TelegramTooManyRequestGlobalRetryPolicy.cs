using System.Net;
using Talkie.Bridges.Telegram.Exceptions;

namespace Talkie.Bridges.Telegram.Policies;

public sealed class TelegramTooManyRequestGlobalRetryPolicy(TimeSpan defaultDelay) : ITelegramRetryPolicy
{
    private const int IsDelaying = 1;

    private const int IsNotDelaying = 0;

    private static TaskCompletionSource<bool> _delaySource = new();

    private static int _delayingState = IsNotDelaying;

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

                var currentDelay = defaultDelay;

                if (exception.Parameters.TryGetValue(TelegramException.ParameterNames.RetryAfter, out var delay)
                    && delay.TryGetNumber(out var delaySeconds))
                {
                    currentDelay = TimeSpan.FromSeconds(delaySeconds);

                    if (currentDelay < defaultDelay) currentDelay = defaultDelay;
                }

                await Task.Delay(currentDelay, cancellationToken);

                return true;
            }
            finally
            {
                _delaySource.SetResult(true);
                Interlocked.Exchange(ref _delayingState, IsNotDelaying);
            }
        }

        await _delaySource.Task;

        cancellationToken.ThrowIfCancellationRequested();

        return true;
    }
}
