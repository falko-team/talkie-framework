using Talkie.Models.Messages.Contents;

namespace Talkie.Models.Messages;

public interface IMessage
{
    MessageContent Content { get; }
}
