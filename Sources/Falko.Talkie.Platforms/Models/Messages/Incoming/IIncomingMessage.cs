using Talkie.Models.Identifiers;
using Talkie.Models.Messages.Attachments;
using Talkie.Models.Profiles;
using Talkie.Platforms;
using Talkie.Sequences;

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

    IReadOnlySequence<IMessageAttachment> Attachments { get; }

    IIncomingMessageMutator ToMutator();
}
