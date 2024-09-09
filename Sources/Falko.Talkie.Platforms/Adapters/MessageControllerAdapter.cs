using Talkie.Controllers;
using Talkie.Controllers.MessageControllers;
using Talkie.Handlers;
using Talkie.Signals;

namespace Talkie.Adapters;

public sealed class MessageControllerAdapter : SignalContextAdapter<MessagePublishedSignal, IMessageController>
{
    public override IMessageController Adapt(ISignalContext<MessagePublishedSignal> context)
    {
        var message = context.Signal.Message;

        var controllerCreator = message.Platform.ControllerCreator;
        var environmentProfileIdentifier = context.Signal.Message.EnvironmentProfile.Identifier;

        return controllerCreator.CreateMessageController(environmentProfileIdentifier);
    }
}
