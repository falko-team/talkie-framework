using Falko.Talkie.Controllers;

namespace Falko.Talkie.Platforms;

public static partial class PlatformExtensions
{
    public static IControllerFactory<TController, TControllerContext> GetControllerFactory<TController, TControllerContext>
    (
        this IPlatform platform
    ) where TController : class, IController<TControllerContext> where TControllerContext : notnull
    {
        return platform is IWithControllerFactory<TController, TControllerContext> mixin
            ? mixin.Factory
            : throw new NotSupportedException();
    }
}
