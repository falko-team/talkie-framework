using Falko.Talkie.Bridges.Telegram.Models;

namespace Falko.Talkie.Bridges.Telegram.Requests;

public sealed class TelegramSendPhotoRequest
(
    long chatId,
    string photo,
    long? messageThreadId = null,
    string? businessConnectionId = null,
    TelegramReplyParameters? replyParameters = null,
    string? caption = null,
    IReadOnlyCollection<TelegramMessageEntity>? captionEntities = null,
    bool? disableNotification = null
) : ITelegramRequest<TelegramMessage>
{
    public long ChatId => chatId;

    public string Photo => photo;

    public long? MessageThreadId => messageThreadId;

    public string? BusinessConnectionId => businessConnectionId;

    public TelegramReplyParameters? ReplyParameters => replyParameters;

    public string? Caption => caption;

    public IReadOnlyCollection<TelegramMessageEntity>? CaptionEntities => captionEntities;

    public bool? DisableNotification => disableNotification;
}
