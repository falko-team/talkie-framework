using Talkie.Localizations;
using Talkie.Models.Identifiers;

namespace Talkie.Models.Profiles;

public sealed record TelegramChatProfile : IChatProfile
{
    public required Identifier Identifier { get; init; }

    public Language Language { get; init; }

    public string? Title { get; init; }

    public string? NickName { get; init; }

    public string? Description { get; init; }
}
