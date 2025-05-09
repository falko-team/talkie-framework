using Talkie.Controllers;
using Talkie.Controllers.CommandControllers;

namespace Talkie.Platforms;

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
