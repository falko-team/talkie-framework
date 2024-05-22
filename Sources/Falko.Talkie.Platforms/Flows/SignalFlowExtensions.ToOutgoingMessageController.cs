using Talkie.Adapters;
using Talkie.Controllers;
using Talkie.Handlers;
using Talkie.Signals;

namespace Talkie.Flows;

public static partial class SignalFlowExtensions
{
    public static IOutgoingMessageController ToMessageController(this ISignalContext<IncomingMessageSignal> context)
    {
        return context.To<OutgoingMessageControllerAdapter, IOutgoingMessageController>();
    }
}
