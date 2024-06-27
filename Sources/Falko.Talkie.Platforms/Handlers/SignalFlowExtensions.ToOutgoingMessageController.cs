using Talkie.Adapters;
using Talkie.Controllers;
using Talkie.Signals;

namespace Talkie.Handlers;

public static partial class SignalContextExtensions
{
    public static IOutgoingMessageController ToMessageController(this ISignalContext<IncomingMessageSignal> context)
    {
        return context.To<OutgoingMessageControllerAdapter, IOutgoingMessageController>();
    }
}
