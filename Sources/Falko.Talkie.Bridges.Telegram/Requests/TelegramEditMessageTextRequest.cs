using Talkie.Bridges.Telegram.Models;

namespace Talkie.Bridges.Telegram.Requests;

public sealed class TelegramEditMessageTextRequest
(
    string text,
    long? chatId = null,
    long? messageId = null,
    string? businessConnectionId = null,
    IReadOnlyCollection<TelegramMessageEntity>? entities = null
)
{
    public readonly string Text = text;

    public readonly long? ChatId = chatId;

    public readonly long? MessageId = messageId;

    public readonly string? BusinessConnectionId = businessConnectionId;

    public readonly IReadOnlyCollection<TelegramMessageEntity>? Entities = entities;
}
