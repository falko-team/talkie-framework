using Falko.Talkie.Models.Identifiers;
using Falko.Talkie.Models.Messages.Attachments.Variants;
using Falko.Talkie.Models.Messages.Contents;
using Falko.Talkie.Sequences;

namespace Falko.Talkie.Models.Messages.Attachments;

public sealed record MessageImageAttachment : IMessageImageAttachment
{
    private IReadOnlySequence<IMessageFileVariant>? _fileVariants;

    public required IMessageAttachmentIdentifier Identifier { get; init; }

    public MessageContent Content { get; init; } =
        MessageContent.Empty;

    public IReadOnlySequence<IMessageImageVariant> Variants { get; init; } =
        FrozenSequence.Empty<IMessageImageVariant>();

    IReadOnlySequence<IMessageFileVariant> IMessageFileAttachment.Variants =>
        _fileVariants ??= GetFileVariants();

    private IReadOnlySequence<IMessageFileVariant> GetFileVariants()
    {
        return Variants
            .OfType<IMessageFileVariant>()
            .ToFrozenSequence();
    }
}
