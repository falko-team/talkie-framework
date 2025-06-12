using Falko.Talkie.Bridges.Telegram.Models;

namespace Falko.Talkie.Bridges.Telegram.Requests;

public sealed class TelegramSendAudioRequest
(
    long chatId,
    string audio,
    long? messageThreadId = null,
    string? businessConnectionId = null,
    string? title = null,
    string? performer = null,
    int? duration = null,
    string? thumbnail = null,
    TelegramReplyParameters? replyParameters = null,
    string? caption = null,
    IReadOnlyCollection<TelegramMessageEntity>? captionEntities = null,
    bool? disableNotification = null
) : ITelegramRequest<TelegramMessage>
{
    public long ChatId => chatId;

    public string Audio => audio;

    public long? MessageThreadId => messageThreadId;

    public string? BusinessConnectionId => businessConnectionId;

    public string? Title => title;

    public string? Performer => performer;

    public long? Duration => duration;

    public string? Thumbnail => thumbnail;

    public TelegramReplyParameters? ReplyParameters => replyParameters;

    public string? Caption => caption;

    public IReadOnlyCollection<TelegramMessageEntity>? CaptionEntities => captionEntities;

    public bool? DisableNotification => disableNotification;
}
