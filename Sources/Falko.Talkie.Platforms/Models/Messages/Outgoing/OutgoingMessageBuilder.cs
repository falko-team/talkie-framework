using Talkie.Models.Messages.Contents;

namespace Talkie.Models.Messages.Outgoing;

public sealed class OutgoingMessageBuilder : IOutgoingMessageBuilder
{
    public MessageContent Content { get; private set; }

    public GlobalIdentifier? Reply { get; private set; }

    public IOutgoingMessageBuilder SetReply(GlobalIdentifier reply)
    {
        Reply = reply;

        return this;
    }

    public IOutgoingMessageBuilder SetContent(MessageContent content)
    {
        Content = content;

        return this;
    }

    public IOutgoingMessage Build()
    {
        if (Content.IsEmpty) return OutgoingMessage.Empty;

        return new OutgoingMessage
        {
            Content = Content,
            Reply = Reply
        };
    }
}
