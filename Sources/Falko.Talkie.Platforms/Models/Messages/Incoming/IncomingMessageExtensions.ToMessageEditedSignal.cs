using Talkie.Signals;

namespace Talkie.Models.Messages.Incoming;

public static partial class IncomingMessageExtensions
{
    public static MessageExchangedSignal ToMessageExchangedSignal(this IIncomingMessage message)
    {
        return new MessageExchangedSignal(message);
    }
}
