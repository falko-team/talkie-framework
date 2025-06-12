using Falko.Talkie.Flows;
using Falko.Talkie.Models.Identifiers;
using Falko.Talkie.Platforms;

namespace Falko.Talkie.Controllers.MessageControllers;

public sealed class TelegramMessageControllerFactory
(
    ISignalFlow flow,
    TelegramPlatform platform
) : IControllerFactory<IMessageController, GlobalMessageIdentifier>
{
    public IMessageController Create(GlobalMessageIdentifier identifier)
    {
        return new TelegramMessageController(flow, platform, identifier);
    }
}
