using Talkie.Signals;

namespace Talkie.Models.Messages.Incoming;

public static partial class IncomingMessageExtensions
{
    public static MessagePublishedSignal ToMessageReceivedSignal(this IIncomingMessage message)
    {
        return new MessagePublishedSignal(message);
    }
}
