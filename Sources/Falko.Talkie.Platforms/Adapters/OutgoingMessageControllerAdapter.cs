using Talkie.Controllers;
using Talkie.Handlers;
using Talkie.Signals;

namespace Talkie.Adapters;

public sealed class OutgoingMessageControllerAdapter : SignalContextAdapter<IncomingMessageSignal, IOutgoingMessageController>
{
    public override IOutgoingMessageController Adapt(ISignalContext<IncomingMessageSignal> context)
    {
        var message = context.Signal.Message;

        var controllerCreator = message.Platform.ControllerCreator;
        var environmentProfileIdentifier = context.Signal.Message.EnvironmentProfile.Identifier;

        return controllerCreator.CreateOutgoingMessageController(environmentProfileIdentifier);
    }
}
