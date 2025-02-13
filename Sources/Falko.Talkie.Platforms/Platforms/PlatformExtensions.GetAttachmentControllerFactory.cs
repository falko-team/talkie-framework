using Talkie.Common;
using Talkie.Controllers;
using Talkie.Controllers.AttachmentControllers;

namespace Talkie.Platforms;

public static partial class PlatformExtensions
{
    public static IControllerFactory<IAttachmentController, Unit> GetAttachmentControllerFactory
    (
        this IPlatform platform
    )
    {
        return platform.GetControllerFactory<IAttachmentController, Unit>();
    }
}
