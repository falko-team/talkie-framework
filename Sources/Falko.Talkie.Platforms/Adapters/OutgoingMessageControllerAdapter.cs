using Falko.Talkie.Controllers;
using Falko.Talkie.Handlers;
using Falko.Talkie.Models.Messages;
using Falko.Talkie.Signals;

namespace Falko.Talkie.Adapters;

public sealed class OutgoingMessageControllerAdapter : SignalContextAdapter<IncomingMessageSignal, IOutgoingMessageController>
{
    public override IOutgoingMessageController Adapt(ISignalContext<IncomingMessageSignal> context)
    {
        var message = context.Signal.Message;

        return message.Platform.ControllerCreator.Create<IOutgoingMessageController, IIncomingMessage>(message);
    }
}
