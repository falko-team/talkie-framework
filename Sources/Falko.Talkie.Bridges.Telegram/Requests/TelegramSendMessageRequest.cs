using Falko.Talkie.Bridges.Telegram.Models;

namespace Falko.Talkie.Bridges.Telegram.Requests;

public sealed class TelegramSendMessageRequest
(
    long chatId,
    string text,
    string? businessConnectionId = null,
    IReadOnlyCollection<TelegramMessageEntity>? entities = null,
    bool? disableNotification = null,
    TelegramReplyParameters? replyParameters = null
) : ITelegramRequest<TelegramMessage>
{
    public long ChatId => chatId;

    public string? BusinessConnectionId => businessConnectionId;

    public string Text => text;

    public IReadOnlyCollection<TelegramMessageEntity>? Entities => entities;

    public bool? DisableNotification => disableNotification;

    public TelegramReplyParameters? ReplyParameters => replyParameters;
}
