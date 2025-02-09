using Talkie.Controllers;
using Talkie.Controllers.MessageControllers;
using Talkie.Models.Identifiers;

namespace Talkie.Platforms;

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
