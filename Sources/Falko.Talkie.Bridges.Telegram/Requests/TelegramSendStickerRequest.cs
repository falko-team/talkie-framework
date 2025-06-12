using Falko.Talkie.Bridges.Telegram.Models;

namespace Falko.Talkie.Bridges.Telegram.Requests;

public sealed class TelegramSendStickerRequest
(
    long chatId,
    string sticker,
    long? messageThreadId = null,
    string? businessConnectionId = null,
    bool? disableNotification = null,
    TelegramReplyParameters? replyParameters = null
) : ITelegramRequest<TelegramMessage>
{
    public long ChatId => chatId;

    public string Sticker => sticker;

    public long? MessageThreadId => messageThreadId;

    public string? BusinessConnectionId => businessConnectionId;

    public bool? DisableNotification => disableNotification;

    public TelegramReplyParameters? ReplyParameters => replyParameters;
}
