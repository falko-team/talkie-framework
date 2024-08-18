using Talkie.Models.Messages.Contents;

namespace Talkie.Models.Messages.Outgoing;

public interface IOutgoingMessageBuilder
{
    MessageContent Content { get; }

    GlobalIdentifier? Reply { get; }

    IOutgoingMessageBuilder SetReply(GlobalIdentifier reply);

    IOutgoingMessageBuilder SetContent(MessageContent content);

    IOutgoingMessage Build();
}
