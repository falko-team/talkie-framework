using System.Text.Json.Serialization;

namespace Talkie.Bridges.Telegram.Models;

public sealed class SendMessage(
    long chatId,
    string text,
    IReadOnlyCollection<MessageEntity>? entities = null,
    bool? disableNotification = null,
    ReplyParameters? replyParameters = null)
{
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public readonly long ChatId = chatId;

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public readonly string Text = text;

    public readonly IReadOnlyCollection<MessageEntity>? Entities = entities;

    public readonly bool? DisableNotification = disableNotification;

    public readonly ReplyParameters? ReplyParameters = replyParameters;
}
