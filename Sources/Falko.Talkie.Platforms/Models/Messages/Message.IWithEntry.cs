using Talkie.Models.Entries;

namespace Talkie.Models.Messages;

public static partial class Message
{
    public interface IWithEntry : IMessage
    {
        IEntry Entry { get; }
    }
}
