using Talkie.Controllers;

namespace Talkie.Platforms;

public interface IPlatform
{
    IControllerCreator ControllerCreator { get; }
}
