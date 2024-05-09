namespace Falko.Unibot.Models.Messages;

public static partial class XMessage
{
    public interface IWithReply : IMessage
    {
        IMessage? Reply { get; }
    }
}
