using System.Net;
using Talkie.Bridges.Telegram.Exceptions;

namespace Talkie.Bridges.Telegram.Policies;

public sealed class TelegramTooManyRequestLocalRetryPolicy(TimeSpan minimumDelay = default) : ITelegramRetryPolicy
{
    private readonly TimeSpan _minimumDelay = minimumDelay <= TimeSpan.Zero
        ? TimeSpan.FromSeconds(3)
        : minimumDelay;

    public ValueTask<bool> EvaluateAsync(TelegramException exception, CancellationToken cancellationToken)
    {
        return exception.StatusCode is HttpStatusCode.TooManyRequests
            ? ProcessExceptionAsync(exception, cancellationToken)
            : ValueTask.FromResult(false);
    }

    private async ValueTask<bool> ProcessExceptionAsync(TelegramException exception, CancellationToken cancellationToken)
    {
        var currentDelay = _minimumDelay;

        if (exception.Parameters.TryGetValue(TelegramException.ParameterNames.RetryAfter, out var delayValue))
        if (delayValue.TryGetNumber(out var delaySeconds))
        {
            var exceptionDelay = TimeSpan.FromSeconds(delaySeconds);

            if (exceptionDelay > _minimumDelay) currentDelay = exceptionDelay;
        }

        await Task.Delay(currentDelay, cancellationToken);

        return true;
    }
}
