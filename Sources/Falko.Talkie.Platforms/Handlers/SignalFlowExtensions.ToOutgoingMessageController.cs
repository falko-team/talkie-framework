using Talkie.Adapters;
using Talkie.Controllers;
using Talkie.Models;
using Talkie.Signals;

namespace Talkie.Handlers;

public static partial class SignalContextExtensions
{
    public static IOutgoingMessageController ToMessageController(this ISignalContext<IncomingMessageSignal> context)
    {
        return context.To<OutgoingMessageControllerAdapter, IOutgoingMessageController>();
    }

    public static IOutgoingMessageController CreateMessageController(this ISignalContext<IncomingMessageSignal> context,
        Identifier environmentProfileIdentifier)
    {
        return context.Signal.Message.Platform
            .ControllerCreator.CreateOutgoingMessageController(environmentProfileIdentifier);
    }
}
