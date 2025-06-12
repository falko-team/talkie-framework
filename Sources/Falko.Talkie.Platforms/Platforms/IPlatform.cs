using Falko.Talkie.Models.Identifiers;

namespace Falko.Talkie.Platforms;

public interface IPlatform
{
    IIdentifier Identifier { get; }
}
