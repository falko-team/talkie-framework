using Talkie.Localizations;
using Talkie.Models.Identifiers;

namespace Talkie.Models.Profiles;

public interface IProfile
{
    IProfileIdentifier Identifier { get; }

    Language Language { get; }
}
