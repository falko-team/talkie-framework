using Talkie.Common;
using Talkie.Controllers;
using Talkie.Controllers.AttachmentControllers;

namespace Talkie.Platforms;

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
