namespace Falko.Talkie.Bridges.Telegram.Models;

public sealed class MessageOriginChat(
    MessageOriglongype type,
    DateTime date,
    Chat senderChat,
    string? authorSignature = null) : MessageOrigin(type, date)
{
    public readonly Chat SenderChat = senderChat;

    public readonly string? AuthorSignature = authorSignature;
}
