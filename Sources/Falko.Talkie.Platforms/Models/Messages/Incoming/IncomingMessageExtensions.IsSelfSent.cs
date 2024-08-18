namespace Talkie.Models.Messages.Incoming;

public static partial class IncomingMessageExtensions
{
    public static bool IsSelfSent(this IIncomingMessage message)
    {
        return message.ReceiverProfile.Identifier == message.SenderProfile.Identifier;
    }
}
