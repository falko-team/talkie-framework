using Talkie.Models.Messages.Attachments.Factories;
using Talkie.Models.Messages.Contents;
using Talkie.Models.Messages.Features;

namespace Talkie.Models.Messages;

public interface IMessage
{
    IEnumerable<IMessageFeature> Features { get; }

    MessageContent Content { get; }
}
