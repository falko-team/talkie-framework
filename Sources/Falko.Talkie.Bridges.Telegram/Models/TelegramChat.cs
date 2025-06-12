namespace Falko.Talkie.Bridges.Telegram.Models;

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
    public long Id => id;

    public TelegramChatType Type => type;

    public string? Title => title;

    public string? FirstName => firstName;

    public string? LastName => lastName;

    public string? Username => username;

    public bool? IsForum => isForum;
}
