using Talkie.Models.Messages.Incoming;
using Talkie.Signals;

namespace Talkie.Handlers;

public static partial class SignalContextExtensions
{
    public static IIncomingMessage GetMessage(this ISignalContext<MessagePublishedSignal> context)
    {
        return context.Signal.Message;
    }

    public static IIncomingMessage GetMessage(this ISignalContext<MessageExchangedSignal> context)
    {
        return context.Signal.Message;
    }
}
