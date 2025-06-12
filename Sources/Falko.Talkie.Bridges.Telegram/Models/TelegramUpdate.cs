namespace Falko.Talkie.Bridges.Telegram.Models;

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
    public int UpdateId => updateId;

    public TelegramMessage? Message => message;

    public TelegramMessage? EditedMessage => editedMessage;

    public TelegramMessage? ChannelPost => channelPost;

    public TelegramMessage? EditedChannelPost => editedChannelPost;

    public TelegramMessage? BusinessMessage => businessMessage;

    public TelegramMessage? EditedBusinessMessage => editedBusinessMessage;
}
