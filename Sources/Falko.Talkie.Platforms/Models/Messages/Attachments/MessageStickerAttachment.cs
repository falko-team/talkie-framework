using Talkie.Models.Identifiers;
using Talkie.Models.Messages.Attachments.Variants;
using Talkie.Sequences;

namespace Talkie.Models.Messages.Attachments;

public sealed record MessageStickerAttachment : IMessageStickerAttachment
{
    private IReadOnlySequence<IMessageFileVariant>? _fileVariants;

    public required Identifier Identifier { get; init; }

    public IReadOnlySequence<IMessageImageVariant> Variants { get; init; } =
        FrozenSequence<IMessageImageVariant>.Empty;

    IReadOnlySequence<IMessageFileVariant> IMessageFileAttachment.Variants =>
        _fileVariants ??= GetFileVariants();

    private IReadOnlySequence<IMessageFileVariant> GetFileVariants()
    {
        return Variants
            .OfType<IMessageFileVariant>()
            .ToFrozenSequence();
    }
}
