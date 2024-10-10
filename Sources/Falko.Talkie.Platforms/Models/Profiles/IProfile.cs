using Talkie.Localizations;
using Talkie.Models.Identifiers;

namespace Talkie.Models.Profiles;

public interface IProfile
{
    Identifier Identifier { get; }

    Language Language { get; }
}
