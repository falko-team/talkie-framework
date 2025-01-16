using Talkie.Models.Identifiers;
using Talkie.Models.Messages.Contents;
using Talkie.Models.Messages.Features;
using Talkie.Sequences;

namespace Talkie.Models.Messages.Outgoing;

public sealed record OutgoingMessage : IOutgoingMessage
{
    public static readonly OutgoingMessage Empty = new();

    public IEnumerable<IMessageFeature> Features { get; init; } = FrozenSequence<IMessageFeature>.Empty;

    public MessageContent Content { get; init; }

    public GlobalMessageIdentifier? Reply { get; init; }

    public IOutgoingMessageMutator ToMutator() => new OutgoingMessageMutator(this);
}
