using System.Net;
using Talkie.Bridges.Telegram.Exceptions;

namespace Talkie.Bridges.Telegram.Policies;

public sealed class TelegramTooManyRequestLocalRetryPolicy(TimeSpan defaultDelay) : ITelegramRetryPolicy
{
    public async ValueTask<bool> EvaluateAsync(TelegramException exception, CancellationToken cancellationToken)
    {
        if (exception.StatusCode is not HttpStatusCode.TooManyRequests)
        {
            return false;
        }

        cancellationToken.ThrowIfCancellationRequested();

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
}
