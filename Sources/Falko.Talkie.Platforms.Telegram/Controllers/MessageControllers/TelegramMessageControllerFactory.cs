using Talkie.Flows;
using Talkie.Models.Identifiers;
using Talkie.Platforms;

namespace Talkie.Controllers.MessageControllers;

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
