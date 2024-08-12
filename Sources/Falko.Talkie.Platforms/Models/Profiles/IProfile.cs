using Talkie.Globalization;

namespace Talkie.Models.Profiles;

public interface IProfile
{
    Identifier Identifier { get; }

    Language Language { get; }
}
