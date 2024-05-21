namespace Falko.Talkie.Bridges.Telegram.Models;

public sealed class GetUpdates(
    long? offset = null,
    long? limit = null,
    long? timeout = null,
    IEnumerable<Update>? allowedUpdates = null)
{
    public readonly long? Offset = offset;

    public readonly long? Limit = limit;

    public readonly long? Timeout = timeout;

    public readonly IEnumerable<Update>? AllowedUpdates = allowedUpdates;
}
