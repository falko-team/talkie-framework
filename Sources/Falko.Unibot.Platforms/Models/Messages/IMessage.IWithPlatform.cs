using Falko.Unibot.Platforms;

namespace Falko.Unibot.Models.Messages;

public static partial class XMessage
{
    public interface IWithPlatform : IMessage
    {
        IPlatform Platform { get; }
    }
}
