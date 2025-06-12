using System.Net;
using Falko.Talkie.Bridges.Telegram.Exceptions;

namespace Falko.Talkie.Bridges.Telegram.Policies;

public sealed class TelegramTooManyRequestGlobalRetryPolicy(TimeSpan minimumDelay = default) : ITelegramRetryPolicy
{
    private const int IsDelayingState = 1;

    private const int IsNotDelayingState = 0;

    private readonly TimeSpan _minimumDelay = minimumDelay <= TimeSpan.Zero
        ? TimeSpan.FromSeconds(3)
        : minimumDelay;

    private TaskCompletionSource _delaySource = new();

    private int _delayingState = IsNotDelayingState;

    public ValueTask<bool> EvaluateAsync(TelegramException exception, CancellationToken cancellationToken)
    {
        return exception.StatusCode is HttpStatusCode.TooManyRequests
            ? ProcessExceptionAsync(exception, cancellationToken)
            : ValueTask.FromResult(false);
    }

    private async ValueTask<bool> ProcessExceptionAsync(TelegramException exception, CancellationToken cancellationToken)
    {
        if (Interlocked.CompareExchange(ref _delayingState, IsDelayingState, IsNotDelayingState) is IsNotDelayingState)
        {
            try
            {
                var currentDelay = _minimumDelay;

                if (exception.Parameters.TryGetValue(TelegramException.ParameterNames.RetryAfter, out var delayValue))
                if (delayValue.TryGetNumber(out var delaySeconds))
                {
                    var exceptionDelay = TimeSpan.FromSeconds(delaySeconds);

                    if (exceptionDelay > _minimumDelay) currentDelay = exceptionDelay;
                }

                await Task.Delay(currentDelay, cancellationToken);
            }
            finally
            {
                var delaySource = _delaySource;

                _delaySource = new TaskCompletionSource();

                Interlocked.Exchange(ref _delayingState, IsNotDelayingState);

                delaySource.SetResult();
            }

            return true;
        }

        await _delaySource.Task.WaitAsync(cancellationToken);

        return true;
    }
}
