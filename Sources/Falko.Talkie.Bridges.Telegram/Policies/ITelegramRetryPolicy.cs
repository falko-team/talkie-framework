using Falko.Talkie.Bridges.Telegram.Exceptions;

namespace Falko.Talkie.Bridges.Telegram.Policies;

public interface ITelegramRetryPolicy
{
    ValueTask<bool> EvaluateAsync(TelegramException exception, CancellationToken cancellationToken);
}
