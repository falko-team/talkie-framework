using Falko.Talkie.Controllers.CommandControllers;
using Falko.Talkie.Models.Profiles;

namespace Falko.Talkie.Controllers.CommandsControllers;

public sealed class TelegramCommandControllerFactory(IProfile relatedProfile) : IControllerFactory<ICommandController, string>
{
    public ICommandController Create(string text)
    {
        return string.IsNullOrWhiteSpace(text) || text[0] is not '/'
            ? WithoutTextTelegramCommandController.Instance
            : new WithTextTelegramCommandController(relatedProfile, text.AsMemory(1));
    }
}
