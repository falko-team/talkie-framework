using Talkie.Controllers;
using Talkie.Handlers;
using Talkie.Models.Messages;
using Talkie.Signals;

namespace Talkie.Adapters;

public sealed class OutgoingMessageControllerAdapter : SignalContextAdapter<IncomingMessageSignal, IOutgoingMessageController>
{
    public override IOutgoingMessageController Adapt(ISignalContext<IncomingMessageSignal> context)
    {
        var message = context.Signal.Message;

        return message.Platform.ControllerCreator.Create<IOutgoingMessageController, IIncomingMessage>(message);
    }
}
