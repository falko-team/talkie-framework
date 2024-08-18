using System.Text.Json.Serialization;

namespace Talkie.Bridges.Telegram.Models;

public sealed class Update(
    int updateId,
    Message? message = null,
    Message? editedMessage = null,
    Message? channelPost = null,
    Message? editedChannelPost = null,
    Message? businessMessage = null,
    Message? editedBusinessMessage = null)
{
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public readonly int UpdateId = updateId;

    public readonly Message? Message = message;

    public readonly Message? EditedMessage = editedMessage;

    public readonly Message? ChannelPost = channelPost;

    public readonly Message? EditedChannelPost = editedChannelPost;

    public readonly Message? BusinessMessage = businessMessage;

    public readonly Message? EditedBusinessMessage = editedBusinessMessage;
}
