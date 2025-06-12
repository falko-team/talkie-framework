namespace Falko.Talkie.Controllers;

public interface IControllerFactory<out TController, in TControllerContext>
    where TController : class, IController<TControllerContext>
    where TControllerContext : notnull
{
    TController Create(TControllerContext context);
}
