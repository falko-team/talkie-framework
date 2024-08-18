using System.Text.Json.Serialization;

namespace Talkie.Bridges.Telegram.Models;

public sealed class MessageEntity(
    string type,
    int offset,
    int length)
{
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public readonly string Type = type;

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public readonly int Offset = offset;

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public readonly int Length = length;
}
