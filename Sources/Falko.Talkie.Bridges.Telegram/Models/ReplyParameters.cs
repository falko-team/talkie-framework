using System.Text.Json.Serialization;

namespace Talkie.Bridges.Telegram.Models;

public sealed class ReplyParameters(
    long messageId,
    long? chatId = null)
{
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public readonly long MessageId = messageId;

    public readonly long? ChatId = chatId;
}
