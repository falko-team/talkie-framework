using Falko.Talkie.Platforms;

namespace Falko.Talkie.Models.Messages;

public static partial class Message
{
    public interface IWithPlatform : IMessage
    {
        IPlatform Platform { get; }
    }
}
