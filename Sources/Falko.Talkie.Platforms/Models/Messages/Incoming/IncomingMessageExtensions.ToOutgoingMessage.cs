using Talkie.Models.Messages.Outgoing;

namespace Talkie.Models.Messages.Incoming;

public static partial class IncomingMessageExtensions
{
    public static IOutgoingMessage ToOutgoingMessage(this IIncomingMessage message)
    {
        return new OutgoingMessage
        {
            Content = message.Content,
            Reply = message.Reply?.ToGlobalMessageIdentifier()
        };
    }

    public static IOutgoingMessage ToOutgoingMessage(this IMessage message)
    {
        return new OutgoingMessage
        {
            Content = message.Content
        };
    }
}
