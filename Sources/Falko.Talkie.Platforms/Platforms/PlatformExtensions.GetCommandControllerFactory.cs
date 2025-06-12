using Falko.Talkie.Controllers;
using Falko.Talkie.Controllers.CommandControllers;

namespace Falko.Talkie.Platforms;

public static partial class PlatformExtensions
{
    public static IControllerFactory<ICommandController, string> GetCommandControllerFactory
    (
        this IPlatform platform
    )
    {
        return platform.GetControllerFactory<ICommandController, string>();
    }
}
