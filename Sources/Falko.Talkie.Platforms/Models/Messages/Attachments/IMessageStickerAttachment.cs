using Falko.Talkie.Models.Messages.Attachments.Variants;
using Falko.Talkie.Sequences;

namespace Falko.Talkie.Models.Messages.Attachments;

public interface IMessageStickerAttachment : IMessageFileAttachment
{
    new IReadOnlySequence<IMessageImageVariant> Variants { get; }
}
