using Talkie.Bridges.Telegram.Models;

namespace Talkie.Bridges.Telegram.Requests;

public sealed class TelegramSendMediaGroupRequest
(
    long chatId,
    IReadOnlyCollection<TelegramInputMedia> media,
    long? messageThreadId = null,
    string? businessConnectionId = null,
    bool? disableNotification = null,
    TelegramReplyParameters? replyParameters = null
) : ITelegramRequest<IReadOnlyList<TelegramMessage>>
{
    public long ChatId => chatId;

    public IReadOnlyCollection<TelegramInputMedia> Media => media;

    public long? MessageThreadId => messageThreadId;

    public string? BusinessConnectionId => businessConnectionId;

    public bool? DisableNotification => disableNotification;

    public TelegramReplyParameters? ReplyParameters => replyParameters;
}
