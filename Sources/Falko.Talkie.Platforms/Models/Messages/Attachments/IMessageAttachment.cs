using Falko.Talkie.Models.Identifiers;

namespace Falko.Talkie.Models.Messages.Attachments;

public interface IMessageAttachment
{
    IMessageAttachmentIdentifier Identifier { get; }
}
