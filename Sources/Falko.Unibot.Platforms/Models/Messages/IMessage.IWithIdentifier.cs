namespace Falko.Unibot.Models.Messages;

public static partial class XMessage
{
    public interface IWithIdentifier : IMessage
    {
        Identifier Id { get; }
    }
}
