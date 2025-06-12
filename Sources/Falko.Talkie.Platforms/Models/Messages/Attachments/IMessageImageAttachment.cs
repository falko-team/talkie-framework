using Falko.Talkie.Models.Messages.Attachments.Variants;
using Falko.Talkie.Models.Messages.Contents;
using Falko.Talkie.Sequences;

namespace Falko.Talkie.Models.Messages.Attachments;

public interface IMessageImageAttachment : IMessageFileAttachment
{
    MessageContent Content { get; }

    new IReadOnlySequence<IMessageImageVariant> Variants { get; }
}
