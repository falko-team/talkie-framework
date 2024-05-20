using Falko.Unibot.Controllers;

namespace Falko.Unibot.Platforms;

public interface IPlatform
{
    IControllerCreator ControllerCreator { get; }
}
