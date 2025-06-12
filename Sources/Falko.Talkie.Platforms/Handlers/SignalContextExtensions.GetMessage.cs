using Falko.Talkie.Models.Messages.Incoming;
using Falko.Talkie.Signals;

namespace Falko.Talkie.Handlers;

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
