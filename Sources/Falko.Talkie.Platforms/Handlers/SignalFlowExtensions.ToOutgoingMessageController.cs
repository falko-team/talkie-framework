using Talkie.Adapters;
using Talkie.Controllers;
using Talkie.Controllers.OutgoingMessageControllers;
using Talkie.Models;
using Talkie.Models.Messages.Incoming;
using Talkie.Signals;

namespace Talkie.Handlers;

public static partial class SignalContextExtensions
{
    public static IOutgoingMessageController ToMessageController(this ISignalContext<IncomingMessageSignal> context)
    {
        return context.To<OutgoingMessageControllerAdapter, IOutgoingMessageController>();
    }

    public static IOutgoingMessageController GetMessageController(this ISignalContext<IncomingMessageSignal> context,
        Identifier environmentProfileIdentifier)
    {
        return context.GetMessage()
            .Platform
            .ControllerCreator
            .CreateOutgoingMessageController(environmentProfileIdentifier);
    }

    public static IIncomingMessage GetMessage(this ISignalContext<IncomingMessageSignal> context)
    {
        return context.Signal.Message;
    }
}
