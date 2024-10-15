using Talkie.Models.Messages.Attachments.Variants;
using Talkie.Sequences;

namespace Talkie.Models.Messages.Attachments;

public interface IMessageStickerAttachment : IMessageFileAttachment
{
    new IReadOnlySequence<IMessageImageVariant> Variants { get; }
}
