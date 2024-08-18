using System.Text.Json.Serialization;

namespace Talkie.Bridges.Telegram.Models;

public sealed class User(
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
    bool? canConnectToBusiness = null)
{
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public readonly long Id = id;

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public readonly bool IsBot = isBot;

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public readonly string FirstName = firstName;

    public readonly string? LastName = lastName;

    public readonly string? Username = username;

    public readonly string? LanguageCode = languageCode;

    public readonly bool? IsPremium = isPremium;

    public readonly bool? AddedToAttachmentMenu = addedToAttachmentMenu;

    public readonly bool? CanJoinGroups = canJoinGroups;

    public readonly bool? CanReadAllGroupMessages = canReadAllGroupMessages;

    public readonly bool? SupportsInlineQueries = supportsInlineQueries;

    public readonly bool? CanConnectToBusiness = canConnectToBusiness;
}
