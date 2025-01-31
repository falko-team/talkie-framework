namespace Talkie.Bridges.Telegram.Models;

public sealed class TelegramMessageEntity
(
    string type,
    int offset,
    int length,
    string? url = null
)
{
    public string Type => type;

    public int Offset => offset;

    public int Length => length;

    public string? Url => url;
}
