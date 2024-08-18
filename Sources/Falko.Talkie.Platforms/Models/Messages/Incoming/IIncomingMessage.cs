using Talkie.Models.Profiles;
using Talkie.Platforms;

namespace Talkie.Models.Messages.Incoming;

public interface IIncomingMessage : IMessage
{
    Identifier Identifier { get; }

    IPlatform Platform { get; }

    IProfile EnvironmentProfile { get; }

    IProfile SenderProfile { get; }

    DateTime Sent { get; }

    IProfile ReceiverProfile { get; }

    DateTime Received { get; }

    IIncomingMessage? Reply { get; }

    IIncomingMessageMutator ToMutator();
}
