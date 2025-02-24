using Talkie.Models.Identifiers;
using Talkie.Models.Messages.Attachments.Factories;
using Talkie.Models.Messages.Contents;
using Talkie.Models.Messages.Features;
using Talkie.Sequences;

namespace Talkie.Models.Messages.Outgoing;

public sealed record OutgoingMessage : IOutgoingMessage
{
    public static readonly OutgoingMessage Empty = new();

    public IEnumerable<IMessageFeature> Features { get; init; } = FrozenSequence.Empty<IMessageFeature>();

    public MessageContent Content { get; init; } = MessageContent.Empty;

    public GlobalMessageIdentifier? Reply { get; init; }

    public IEnumerable<IMessageAttachmentFactory> Attachments { get; init; } = FrozenSequence.Empty<IMessageAttachmentFactory>();

    public IOutgoingMessageMutator ToMutator() => new OutgoingMessageMutator(this);
}
