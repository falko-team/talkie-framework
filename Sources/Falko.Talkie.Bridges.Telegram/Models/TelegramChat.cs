namespace Talkie.Bridges.Telegram.Models;

public sealed class TelegramChat
(
    long id,
    TelegramChatType type,
    string? title = null,
    string? firstName = null,
    string? lastName = null,
    string? username = null,
    bool? isForum = null
)
{
    public readonly long Id = id;

    public readonly TelegramChatType Type = type;

    public readonly string? Title = title;

    public readonly string? FirstName = firstName;

    public readonly string? LastName = lastName;

    public readonly string? Username = username;

    public readonly bool? IsForum = isForum;
}
