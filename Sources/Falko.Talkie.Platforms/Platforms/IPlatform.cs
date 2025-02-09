using Talkie.Models.Identifiers;

namespace Talkie.Platforms;

public interface IPlatform
{
    IIdentifier Identifier { get; }
}
