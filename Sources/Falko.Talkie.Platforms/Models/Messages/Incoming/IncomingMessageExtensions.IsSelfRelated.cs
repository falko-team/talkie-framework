namespace Falko.Talkie.Models.Messages.Incoming;

public static partial class IncomingMessageExtensions
{
    /// <summary>
    /// Determines whether the message was published by the same profile that received it.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <returns><c>true</c> if the message was published by the same profile that received it; otherwise, <c>false</c>.</returns>
    public static bool IsSelfRelated(this IIncomingMessage message)
    {
        return Equals(message.ReceiverProfile.Identifier, message.PublisherProfile.Identifier);
    }
}
