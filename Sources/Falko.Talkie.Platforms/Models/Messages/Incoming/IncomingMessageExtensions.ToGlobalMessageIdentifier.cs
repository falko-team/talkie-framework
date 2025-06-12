using Falko.Talkie.Models.Identifiers;

namespace Falko.Talkie.Models.Messages.Incoming;

public static partial class IncomingMessageExtensions
{
    public static GlobalMessageIdentifier ToGlobalMessageIdentifier(this IIncomingMessage message)
    {
        return new GlobalMessageIdentifier(message.EnvironmentProfile.Identifier, message.Identifier);
    }
}
