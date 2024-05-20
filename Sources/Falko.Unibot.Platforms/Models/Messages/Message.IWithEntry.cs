using Falko.Unibot.Models.Entries;

namespace Falko.Unibot.Models.Messages;

public static partial class Message
{
    public interface IWithEntry : IMessage
    {
        IEntry Entry { get; }
    }
}
