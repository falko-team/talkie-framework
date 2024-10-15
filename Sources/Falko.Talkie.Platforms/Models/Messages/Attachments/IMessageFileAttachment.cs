using Talkie.Models.Messages.Attachments.Variants;
using Talkie.Sequences;

namespace Talkie.Models.Messages.Attachments;

public interface IMessageFileAttachment : IMessageAttachment
{
    IReadOnlySequence<IMessageFileVariant> Variants { get; }
}
