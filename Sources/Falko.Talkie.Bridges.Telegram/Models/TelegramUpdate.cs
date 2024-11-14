namespace Talkie.Bridges.Telegram.Models;

public sealed class TelegramUpdate
(
    int updateId,
    TelegramMessage? message = null,
    TelegramMessage? editedMessage = null,
    TelegramMessage? channelPost = null,
    TelegramMessage? editedChannelPost = null,
    TelegramMessage? businessMessage = null,
    TelegramMessage? editedBusinessMessage = null
)
{
    public readonly int UpdateId = updateId;

    public readonly TelegramMessage? Message = message;

    public readonly TelegramMessage? EditedMessage = editedMessage;

    public readonly TelegramMessage? ChannelPost = channelPost;

    public readonly TelegramMessage? EditedChannelPost = editedChannelPost;

    public readonly TelegramMessage? BusinessMessage = businessMessage;

    public readonly TelegramMessage? EditedBusinessMessage = editedBusinessMessage;
}
