using Falko.Talkie.Bridges.Telegram.Models;

namespace Falko.Talkie.Bridges.Telegram.Requests;

public sealed class TelegramGetUpdatesRequest
(
    int? offset = null,
    int? limit = null,
    long? timeout = null,
    IReadOnlyList<string>? allowedUpdates = null
) : ITelegramRequest<IReadOnlyList<TelegramUpdate>>
{
    public long? Offset => offset;

    public long? Limit => limit;

    public long? Timeout => timeout;

    public IReadOnlyList<string>? AllowedUpdates => allowedUpdates;
}
