using Falko.Talkie.Models.Identifiers;
using Falko.Talkie.Models.Messages.Attachments.Factories;

namespace Falko.Talkie.Models.Messages.Outgoing;

public interface IOutgoingMessage : IMessage
{
    GlobalMessageIdentifier? Reply { get; }

    IEnumerable<IMessageAttachmentFactory> Attachments { get; }

    IOutgoingMessageMutator ToMutator();
}
