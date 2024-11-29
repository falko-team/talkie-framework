using Talkie.Models.Messages.Attachments.Variants;
using Talkie.Models.Messages.Contents;
using Talkie.Sequences;

namespace Talkie.Models.Messages.Attachments;

public interface IMessageImageAttachment : IMessageFileAttachment
{
    MessageContent Content { get; }

    new IReadOnlySequence<IMessageImageVariant> Variants { get; }
}
