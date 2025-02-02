using Talkie.Models.Identifiers;
using Talkie.Models.Messages.Attachments;
using Talkie.Models.Profiles;
using Talkie.Platforms;

namespace Talkie.Models.Messages.Incoming;

public interface IIncomingMessage : IMessage
{
    IMessageIdentifier Identifier { get; }

    IPlatform Platform { get; }

    IProfile EnvironmentProfile { get; }

    IProfile PublisherProfile { get; }

    DateTime PublishedDate { get; }

    IProfile ReceiverProfile { get; }

    DateTime ReceivedDate { get; }

    IIncomingMessage? Reply { get; }

    IEnumerable<IMessageAttachment> Attachments { get; }

    IIncomingMessageMutator ToMutator();
}
