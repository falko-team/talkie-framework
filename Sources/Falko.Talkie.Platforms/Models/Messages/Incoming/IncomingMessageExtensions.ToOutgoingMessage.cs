using Talkie.Models.Messages.Outgoing;

namespace Talkie.Models.Messages.Incoming;

public static partial class IncomingMessageExtensions
{
    public static IOutgoingMessage ToOutgoingMessage(this IIncomingMessage message)
    {
        return new OutgoingMessage
        {
            Text = message.Text,
            Reply = message.Reply?.ToGlobalIdentifier()
        };
    }

    public static IOutgoingMessage ToOutgoingMessage(this IMessage message)
    {
        return new OutgoingMessage
        {
            Text = message.Text
        };
    }
}
