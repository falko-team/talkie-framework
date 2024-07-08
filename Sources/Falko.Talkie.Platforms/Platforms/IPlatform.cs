using Talkie.Controllers;
using Talkie.Models;

namespace Talkie.Platforms;

public interface IPlatform
{
    Identifier Identifier { get; }

    IControllerCreator ControllerCreator { get; }
}
