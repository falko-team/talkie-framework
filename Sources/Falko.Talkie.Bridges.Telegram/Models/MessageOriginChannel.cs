namespace Talkie.Bridges.Telegram.Models;

public sealed class MessageOriginChannel(
    MessageOriglongype type,
    DateTime date,
    Chat chat,
    long messageId,
    string? authorSignature = null) : MessageOrigin(type, date)
{
    public readonly Chat Chat = chat;

    public readonly long MessageId = messageId;

    public readonly string? AuthorSignature = authorSignature;
}
