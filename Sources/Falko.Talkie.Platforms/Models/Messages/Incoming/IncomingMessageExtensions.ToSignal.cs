using Talkie.Signals;

namespace Talkie.Models.Messages.Incoming;

public static partial class IncomingMessageExtensions
{
    public static IncomingMessageSignal ToSignal(this IIncomingMessage message)
    {
        return new IncomingMessageSignal(message);
    }
}
