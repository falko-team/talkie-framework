namespace Talkie.Models.Messages;

public interface IMessage
{
    string? Text { get; }

    IMessage? Reply { get; }
}
