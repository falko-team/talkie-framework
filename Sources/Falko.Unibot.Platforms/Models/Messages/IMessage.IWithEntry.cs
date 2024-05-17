using Falko.Unibot.Models.Profiles;

namespace Falko.Unibot.Models.Messages;

public static partial class XMessage
{
    public interface IWithEntry : IMessage
    {
        IProfile Sender { get; }

        DateTime Sent { get; }

        IProfile Receiver { get; }

        DateTime? Received { get; }
    }
}
