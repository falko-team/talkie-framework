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

    public readonly long ChatId = chatId;


    public readonly string Text = text;

    public readonly IReadOnlyCollection<TelegramMessageEntity>? Entities = entities;

    public readonly bool? DisableNotification = disableNotification;

    public readonly TelegramReplyParameters? ReplyParameters = replyParameters;
}
