using System.Text.Json.Serialization;

namespace Talkie.Bridges.Telegram.Models;

public sealed class Chat(
    long id,
    ChatType type,
    string? title = null,
    string? firstName = null,
    string? lastName = null,
    string? username = null,
    bool? isForum = null)
{
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public readonly long Id = id;

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public readonly ChatType Type = type;

    public readonly string? Title = title;

    public readonly string? FirstName = firstName;

    public readonly string? LastName = lastName;

    public readonly string? Username = username;

    public readonly bool? IsForum = isForum;
}
