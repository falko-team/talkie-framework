using Talkie.Models.Identifiers;
using Talkie.Models.Messages.Attachments.Variants;
using Talkie.Sequences;

namespace Talkie.Models.Messages.Attachments;

public sealed record TelegramMessageStickerAttachment : IMessageStickerAttachment
{
    public required Identifier Identifier { get; init; }

    public IReadOnlySequence<TelegramMessageImageVariant> Variants { get; init; }
        = FrozenSequence<TelegramMessageImageVariant>.Empty;

    IReadOnlySequence<IMessageFileVariant> IMessageFileAttachment.Variants =>
        Variants.OfType<IMessageFileVariant>().ToFrozenSequence();

    IReadOnlySequence<IMessageImageVariant> IMessageStickerAttachment.Variants =>
        Variants.OfType<IMessageImageVariant>().ToFrozenSequence();
}
