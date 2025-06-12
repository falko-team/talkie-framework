using Falko.Talkie.Localizations;
using Falko.Talkie.Models.Identifiers;

namespace Falko.Talkie.Models.Profiles;

public interface IProfile
{
    IProfileIdentifier Identifier { get; }

    Language Language { get; }
}
