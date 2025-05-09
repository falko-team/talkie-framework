using Talkie.Controllers.CommandControllers;
using Talkie.Models.Profiles;

namespace Talkie.Controllers.CommandsControllers;

public sealed class TelegramCommandControllerFactory(IProfile relatedProfile) : IControllerFactory<ICommandController, string>
{
    public ICommandController Create(string text)
    {
        return string.IsNullOrWhiteSpace(text) || text[0] is not '/'
            ? WithoutTextTelegramCommandController.Instance
            : new WithTextTelegramCommandController(relatedProfile, text.AsMemory(1));
    }
}
