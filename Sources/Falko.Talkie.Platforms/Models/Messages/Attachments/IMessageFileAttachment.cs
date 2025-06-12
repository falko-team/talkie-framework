using Falko.Talkie.Models.Messages.Attachments.Variants;
using Falko.Talkie.Sequences;

namespace Falko.Talkie.Models.Messages.Attachments;

public interface IMessageFileAttachment : IMessageAttachment
{
    IReadOnlySequence<IMessageFileVariant> Variants { get; }
}
