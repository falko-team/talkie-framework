using Talkie.Models.Identifiers;
using Talkie.Models.Messages.Contents;

namespace Talkie.Models.Messages.Outgoing;

public interface IOutgoingMessageBuilder
{
    MessageContent Content { get; }

    GlobalMessageIdentifier? Reply { get; }

    IOutgoingMessageBuilder SetReply(GlobalMessageIdentifier reply);

    IOutgoingMessageBuilder SetContent(MessageContent content);

    IOutgoingMessage Build();
}
