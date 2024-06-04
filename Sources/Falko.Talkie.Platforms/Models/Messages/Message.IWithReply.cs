namespace Talkie.Models.Messages;

public static partial class Message
{
    public interface IWithReply : IMessage
    {
        IMessage? Reply { get; }
    }
}
