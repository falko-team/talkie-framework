namespace Talkie.Models.Messages.Incoming;

public static partial class IncomingMessageExtensions
{
    /// <summary>
    /// Determines whether the message is older than the specified threshold.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="threshold">The threshold.</param>
    /// <returns><c>true</c> if the message is older than the specified threshold; otherwise, <c>false</c>.</returns>
    public static bool IsOlderThan(this IIncomingMessage message, TimeSpan threshold)
    {
        return message.ReceivedDate - message.PublishedDate > threshold;
    }
}
