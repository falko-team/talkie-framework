using Falko.Talkie.Localizations;
using Falko.Talkie.Models.Identifiers;

namespace Falko.Talkie.Models.Profiles;

public sealed record UserProfile : IUserProfile
{
    public required IProfileIdentifier Identifier { get; init; }

    public Language Language { get; init; }

    public string? FirstName { get; init; }

    public string? LastName { get; init; }

    public string? NickName { get; init; }

    public string? Description { get; init; }
}
