namespace Falko.Talkie.Bridges.Telegram.Models;

public sealed class Update(
    long updateId,
    Message? message = null,
    Message? editedMessage = null,
    Message? channelPost = null,
    Message? editedChannelPost = null,
    Message? businessMessage = null,
    Message? editedBusinessMessage = null)
{
    public readonly long UpdateId = updateId;

    public readonly Message? Message = message;

    public readonly Message? EditedMessage = editedMessage;

    public readonly Message? ChannelPost = channelPost;

    public readonly Message? EditedChannelPost = editedChannelPost;

    public readonly Message? BusinessMessage = businessMessage;

    public readonly Message? EditedBusinessMessage = editedBusinessMessage;
}
