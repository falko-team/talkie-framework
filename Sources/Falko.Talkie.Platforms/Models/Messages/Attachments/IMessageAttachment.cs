using Talkie.Models.Identifiers;

namespace Talkie.Models.Messages.Attachments;

public interface IMessageAttachment
{
    Identifier Identifier { get; }
}
