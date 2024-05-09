using Falko.Unibot.Controllers;
using Falko.Unibot.Models.Profiles;

namespace Falko.Unibot.Platforms;

public interface IPlatform
{
    IBotProfile Self { get; }

    IControllerCreator ControllerCreator { get; }
}
