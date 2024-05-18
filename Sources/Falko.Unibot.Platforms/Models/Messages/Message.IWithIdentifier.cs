namespace Falko.Unibot.Models.Messages;

public static partial class Message
{
    public interface IWithIdentifier : IMessage
    {
        Identifier Id { get; }
    }
}
