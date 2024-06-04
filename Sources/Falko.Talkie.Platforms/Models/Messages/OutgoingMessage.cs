namespace Talkie.Models.Messages;

public sealed class OutgoingMessage : IMessage, Message.IWithReply
{
    public string? Content { get; init; }

    public IMessage? Reply { get; init; }
}
