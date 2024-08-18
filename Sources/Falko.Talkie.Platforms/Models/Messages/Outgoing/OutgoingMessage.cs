using Talkie.Models.Messages.Contents;

namespace Talkie.Models.Messages.Outgoing;

public sealed record OutgoingMessage : IOutgoingMessage
{
    public static readonly OutgoingMessage Empty = new();

    public MessageContent Content { get; init; }

    public GlobalIdentifier? Reply { get; init; }

    public IOutgoingMessageMutator ToMutator() => new OutgoingMessageMutator(this);
}
