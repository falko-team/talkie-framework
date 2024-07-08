namespace Talkie.Models.Messages;

public sealed class OutgoingMessage : IMessage
{
    public static readonly OutgoingMessage Empty = new();

    public string? Text { get; init; }

    public IMessage? Reply { get; init; }
}
