using Talkie.Adapters;
using Talkie.Controllers;
using Talkie.Models;
using Talkie.Models.Messages;
using Talkie.Signals;

namespace Talkie.Handlers;

public static partial class SignalContextExtensions
{
    public static IOutgoingMessageController ToOutgoingMessageController(this ISignalContext<IncomingMessageSignal> context)
    {
        return context.To<OutgoingMessageControllerAdapter, IOutgoingMessageController>();
    }

    public static IOutgoingMessageController GetOutgoingMessageController(this ISignalContext<IncomingMessageSignal> context,
        Identifier environmentProfileIdentifier)
    {
        return context.GetIncomingMessage()
            .Platform
            .ControllerCreator
            .CreateOutgoingMessageController(environmentProfileIdentifier);
    }

    public static IIncomingMessage GetIncomingMessage(this ISignalContext<IncomingMessageSignal> context)
    {
        return context.Signal.Message;
    }
}
