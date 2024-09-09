using Talkie.Signals;

namespace Talkie.Models.Messages.Incoming;

public static partial class IncomingMessageExtensions
{
    public static MessageExchangedSignal ToMessageEditedSignal(this IIncomingMessage message)
    {
        return new MessageExchangedSignal(message);
    }
}
