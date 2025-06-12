using Falko.Talkie.Models.Messages.Contents;
using Falko.Talkie.Models.Messages.Features;

namespace Falko.Talkie.Models.Messages;

public interface IMessage
{
    IEnumerable<IMessageFeature> Features { get; }

    MessageContent Content { get; }
}
