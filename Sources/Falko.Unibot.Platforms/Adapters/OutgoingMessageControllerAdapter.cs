using Falko.Unibot.Controllers;
using Falko.Unibot.Handlers;
using Falko.Unibot.Models.Messages;
using Falko.Unibot.Signals;

namespace Falko.Unibot.Adapters;

public sealed class OutgoingMessageControllerAdapter : SignalContextAdapter<IncomingMessageSignal, IOutgoingMessageController>
{
    public override IOutgoingMessageController Adapt(ISignalContext<IncomingMessageSignal> context)
    {
        var message = context.Signal.Message;

        return message.Platform.ControllerCreator.Create<IOutgoingMessageController, IIncomingMessage>(message);
    }
}
