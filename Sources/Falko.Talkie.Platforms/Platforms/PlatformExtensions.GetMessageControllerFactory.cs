using Falko.Talkie.Controllers;
using Falko.Talkie.Controllers.MessageControllers;
using Falko.Talkie.Models.Identifiers;

namespace Falko.Talkie.Platforms;

public static partial class PlatformExtensions
{
    public static IControllerFactory<IMessageController, GlobalMessageIdentifier> GetMessageControllerFactory
    (
        this IPlatform platform
    )
    {
        return platform.GetControllerFactory<IMessageController, GlobalMessageIdentifier>();
    }
}
