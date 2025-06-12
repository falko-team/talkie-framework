using Falko.Talkie.Models.Identifiers;
using Falko.Talkie.Models.Messages.Attachments.Factories;
using Falko.Talkie.Models.Messages.Contents;
using Falko.Talkie.Models.Messages.Features;
using Falko.Talkie.Sequences;

namespace Falko.Talkie.Models.Messages.Outgoing;

public sealed record OutgoingMessage : IOutgoingMessage
{
    public static readonly OutgoingMessage Empty = new();

    public IEnumerable<IMessageFeature> Features { get; init; } = FrozenSequence.Empty<IMessageFeature>();

    public MessageContent Content { get; init; } = MessageContent.Empty;

    public GlobalMessageIdentifier? Reply { get; init; }

    public IEnumerable<IMessageAttachmentFactory> Attachments { get; init; } = FrozenSequence.Empty<IMessageAttachmentFactory>();

    public IOutgoingMessageMutator ToMutator() => new OutgoingMessageMutator(this);
}
