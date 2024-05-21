using Falko.Talkie.Controllers;

namespace Falko.Talkie.Platforms;

public interface IPlatform
{
    IControllerCreator ControllerCreator { get; }
}
