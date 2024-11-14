namespace Talkie.Bridges.Telegram.Models;

public sealed class TelegramMessageEntity
(
    string type,
    int offset,
    int length
)
{
    public readonly string Type = type;

    public readonly int Offset = offset;

    public readonly int Length = length;
}
