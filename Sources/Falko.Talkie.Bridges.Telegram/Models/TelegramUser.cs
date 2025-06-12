namespace Falko.Talkie.Bridges.Telegram.Models;

public sealed class TelegramUser
(
    long id,
    bool isBot,
    string firstName,
    string? lastName = null,
    string? username = null,
    string? languageCode = null,
    bool? isPremium = null,
    bool? addedToAttachmentMenu = null,
    bool? canJoinGroups = null,
    bool? canReadAllGroupMessages = null,
    bool? supportsInlineQueries = null,
    bool? canConnectToBusiness = null
)
{
    public long Id => id;

    public bool IsBot => isBot;

    public string FirstName => firstName;

    public string? LastName => lastName;

    public string? Username => username;

    public string? LanguageCode => languageCode;

    public bool? IsPremium => isPremium;

    public bool? AddedToAttachmentMenu => addedToAttachmentMenu;

    public bool? CanJoinGroups => canJoinGroups;

    public bool? CanReadAllGroupMessages => canReadAllGroupMessages;

    public bool? SupportsInlineQueries => supportsInlineQueries;

    public bool? CanConnectToBusiness => canConnectToBusiness;
}
