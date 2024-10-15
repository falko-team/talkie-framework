using Talkie.Controllers;
using Talkie.Models;
using Talkie.Models.Identifiers;

namespace Talkie.Platforms;

public interface IPlatform
{
    Identifier Identifier { get; }

    IControllerCreator ControllerCreator { get; }
}
