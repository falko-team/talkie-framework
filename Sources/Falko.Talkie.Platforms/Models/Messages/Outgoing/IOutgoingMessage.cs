using Talkie.Models.Identifiers;
using Talkie.Models.Messages.Attachments.Factories;

namespace Talkie.Models.Messages.Outgoing;

public interface IOutgoingMessage : IMessage
{
    GlobalMessageIdentifier? Reply { get; }

    IEnumerable<IMessageAttachmentFactory> Attachments { get; }

    IOutgoingMessageMutator ToMutator();
}
