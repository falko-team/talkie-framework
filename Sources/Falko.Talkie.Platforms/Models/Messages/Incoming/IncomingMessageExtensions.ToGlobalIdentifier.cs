namespace Talkie.Models.Messages.Incoming;

public static partial class IncomingMessageExtensions
{
    public static GlobalIdentifier ToGlobalIdentifier(this IIncomingMessage message)
    {
        return new GlobalIdentifier(message.EnvironmentProfile.Identifier, message.Identifier);
    }
}
