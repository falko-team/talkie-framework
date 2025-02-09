using Talkie.Controllers;
using Talkie.Mixins;

namespace Talkie.Platforms;

public interface IWithControllerFactory<out TController, in TControllerContext> : IWith<IPlatform>
    where TController : class, IController<TControllerContext>
    where TControllerContext : notnull
{
    IControllerFactory<TController, TControllerContext> Factory { get; }
}
