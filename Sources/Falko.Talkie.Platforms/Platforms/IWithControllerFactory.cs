using Falko.Talkie.Controllers;
using Falko.Talkie.Mixins;

namespace Falko.Talkie.Platforms;

public interface IWithControllerFactory<out TController, in TControllerContext> : IWith<IPlatform>
    where TController : class, IController<TControllerContext>
    where TControllerContext : notnull
{
    IControllerFactory<TController, TControllerContext> Factory { get; }
}
