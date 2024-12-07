using Talkie.Bridges.Telegram.Models;

namespace Talkie.Bridges.Telegram.Requests;

public sealed class TelegramSendMessageRequest
(
    long chatId,
    string text,
    IReadOnlyCollection<TelegramMessageEntity>? entities = null,
    bool? disableNotification = null,
    TelegramReplyParameters? replyParameters = null
)
{

    public long ChatId => chatId;

    public string Text => text;

    public IReadOnlyCollection<TelegramMessageEntity>? Entities => entities;

    public bool? DisableNotification => disableNotification;

    public TelegramReplyParameters? ReplyParameters => replyParameters;
}
