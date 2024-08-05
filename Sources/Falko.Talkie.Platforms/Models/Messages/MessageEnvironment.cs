using Talkie.Models.Profiles;
using Talkie.Platforms;

namespace Talkie.Models.Messages;

public interface IMessageEnvironment
{
    Identifier MessageIdentifier { get; }

    IPlatform Platform { get; }

    IProfile EnvironmentProfile { get; }

    IProfile SenderProfile { get; }

    DateTime Sent { get; }

    IProfile ReceiverProfile { get; }

    DateTime Received { get; }
}
