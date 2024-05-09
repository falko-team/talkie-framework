using Falko.Unibot.Controllers;
using Falko.Unibot.Handlers;
using Falko.Unibot.Models.Messages;
using Falko.Unibot.Signals;

namespace Falko.Unibot.Adapters;

public sealed class OutgoingMessageControllerAdapter : SignalContextAdapter<IncomingMessageSignal, IOutgoingMessageController>
{
    public override IOutgoingMessageController Adapt(ISignalContext<IncomingMessageSignal> context)
    {
        if (context.Signal.Message is XMessage.IWithPlatform platformMessage is false)
        {
            throw new InvalidCastException();
        }

        return platformMessage.Platform.ControllerCreator.Create<IOutgoingMessageController, IMessage>(platformMessage);
    }
}
