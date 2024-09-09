using Talkie.Models.Profiles;
using Talkie.Platforms;

namespace Talkie.Models.Messages.Incoming;

public interface IIncomingMessage : IMessage
{
    Identifier Identifier { get; }

    IPlatform Platform { get; }

    IProfile EnvironmentProfile { get; }

    IProfile PublisherProfile { get; }

    DateTime PublishedDate { get; }

    IProfile ReceiverProfile { get; }

    DateTime ReceivedDate { get; }

    IIncomingMessage? Reply { get; }

    IIncomingMessageMutator ToMutator();
}
