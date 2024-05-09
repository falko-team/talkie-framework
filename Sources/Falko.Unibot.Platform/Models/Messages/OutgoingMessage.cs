namespace Falko.Unibot.Models.Messages;

public sealed class OutgoingMessage : IMessage
{
    public string? Content { get; init; }
}
