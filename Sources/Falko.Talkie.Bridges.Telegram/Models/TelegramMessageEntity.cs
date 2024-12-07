namespace Talkie.Bridges.Telegram.Models;

public sealed class TelegramMessageEntity
(
    string type,
    int offset,
    int length
)
{
    public string Type => type;

    public int Offset => offset;

    public int Length => length;
}
