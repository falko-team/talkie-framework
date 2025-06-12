using Falko.Talkie.Models.Identifiers;
using Falko.Talkie.Models.Messages.Attachments;
using Falko.Talkie.Models.Profiles;
using Falko.Talkie.Platforms;

namespace Falko.Talkie.Models.Messages.Incoming;

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
