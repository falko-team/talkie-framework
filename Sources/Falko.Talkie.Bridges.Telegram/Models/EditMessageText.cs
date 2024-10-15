namespace Talkie.Bridges.Telegram.Models;

public sealed class EditMessageText
(
    string text,
    long? chatId = null,
    long? messageId = null,
    string? businessConnectionId = null,
    IReadOnlyCollection<MessageEntity>? entities = null
)
{
    public readonly string Text = text;

    public readonly long? ChatId = chatId;

    public readonly long? MessageId = messageId;

    public readonly string? BusinessConnectionId = businessConnectionId;

    public readonly IReadOnlyCollection<MessageEntity>? Entities = entities;
}
