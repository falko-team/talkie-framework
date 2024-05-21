using Falko.Talkie.Adapters;
using Falko.Talkie.Controllers;
using Falko.Talkie.Handlers;
using Falko.Talkie.Signals;

namespace Falko.Talkie.Flows;

public static partial class SignalFlowExtensions
{
    public static IOutgoingMessageController ToMessageController(this ISignalContext<IncomingMessageSignal> context)
    {
        return context.To<OutgoingMessageControllerAdapter, IOutgoingMessageController>();
    }
}
