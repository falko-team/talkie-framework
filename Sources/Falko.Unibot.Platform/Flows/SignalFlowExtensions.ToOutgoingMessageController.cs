using Falko.Unibot.Adapters;
using Falko.Unibot.Controllers;
using Falko.Unibot.Handlers;
using Falko.Unibot.Signals;

namespace Falko.Unibot.Flows;

public static partial class SignalFlowExtensions
{
    public static IOutgoingMessageController ToOutgoingMessageController(this ISignalContext<IncomingMessageSignal> context)
    {
        return context.To<OutgoingMessageControllerAdapter, IOutgoingMessageController>();
    }
}
