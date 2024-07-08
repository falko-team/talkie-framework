using Talkie.Models.Profiles;
using Talkie.Platforms;

namespace Talkie.Models.Messages;

public interface IIncomingMessage : IMessage
{
    Identifier Identifier { get; }

    IPlatform Platform { get; }

    IProfile EnvironmentProfile { get; }

    IProfile SenderProfile { get; }

    IProfile ReceiverProfile { get; }

    DateTime Sent { get; }

    DateTime Received { get; }

    IIncomingMessage Mutate(Func<IIncomingMessageMutator, IIncomingMessageMutator> mutation);
}
