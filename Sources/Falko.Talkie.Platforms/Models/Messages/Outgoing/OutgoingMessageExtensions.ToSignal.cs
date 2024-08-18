using Talkie.Signals;

namespace Talkie.Models.Messages.Outgoing;

public static partial class OutgoingMessageExtensions
{
    public static OutgoingMessageSignal ToSignal(this IOutgoingMessage message)
    {
        return new OutgoingMessageSignal(message);
    }
}
