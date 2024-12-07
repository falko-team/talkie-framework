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
    public string Text => text;

    public long? ChatId => chatId;

    public long? MessageId => messageId;

    public string? BusinessConnectionId => businessConnectionId;

    public IReadOnlyCollection<TelegramMessageEntity>? Entities => entities;
}
