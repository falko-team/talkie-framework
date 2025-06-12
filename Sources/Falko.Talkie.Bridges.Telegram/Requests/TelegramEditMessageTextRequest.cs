using Falko.Talkie.Bridges.Telegram.Models;

namespace Falko.Talkie.Bridges.Telegram.Requests;

public sealed class TelegramEditMessageTextRequest
(
    string text,
    long? chatId = null,
    long? messageId = null,
    string? businessConnectionId = null,
    IReadOnlyCollection<TelegramMessageEntity>? entities = null
) : ITelegramRequest<TelegramMessage>
{
    public string Text => text;

    public long? ChatId => chatId;

    public long? MessageId => messageId;

    public string? BusinessConnectionId => businessConnectionId;

    public IReadOnlyCollection<TelegramMessageEntity>? Entities => entities;
}
