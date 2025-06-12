using Falko.Talkie.Common;
using Falko.Talkie.Controllers;
using Falko.Talkie.Controllers.AttachmentControllers;

namespace Falko.Talkie.Platforms;

public static partial class PlatformExtensions
{
    public static IControllerFactory<IMessageAttachmentController, Unit> GetAttachmentControllerFactory
    (
        this IPlatform platform
    )
    {
        return platform.GetControllerFactory<IMessageAttachmentController, Unit>();
    }
}
