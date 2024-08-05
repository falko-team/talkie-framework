namespace Talkie.Models.Messages;

public sealed record OutgoingMessage : IOutgoingMessage
{
    public static readonly OutgoingMessage Empty = new();

    public string? Text { get; init; }

    public GlobalIdentifier? Reply { get; init; }

    public IOutgoingMessageMutator ToMutator() => new OutgoingMessageMutator(this);
}
