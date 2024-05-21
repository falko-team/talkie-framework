using Falko.Talkie.Models.Entries;

namespace Falko.Talkie.Models.Messages;

public static partial class Message
{
    public interface IWithEntry : IMessage
    {
        IEntry Entry { get; }
    }
}
