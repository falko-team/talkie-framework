using Talkie.Models.Identifiers;
using Talkie.Models.Messages.Contents;

namespace Talkie.Models.Messages.Outgoing;

public sealed class OutgoingMessageMutator : IOutgoingMessageMutator
{
    private readonly OutgoingMessage _message;

    private MessageContent _content;

    private GlobalMessageIdentifier? _reply;

    internal OutgoingMessageMutator(OutgoingMessage message)
    {
        _message = message;
        _content = message.Content;
        _reply = message.Reply;
    }

    public IOutgoingMessageMutator MutateReply(Func<GlobalMessageIdentifier?, GlobalMessageIdentifier?> replyMutationFactory)
    {
        _reply = replyMutationFactory(_reply);

        return this;
    }

    public IOutgoingMessageMutator MutateContent(Func<MessageContent, MessageContent> contentMutationFactory)
    {
        _content = contentMutationFactory(_content);

        return this;
    }

    public IOutgoingMessage Mutate()
    {
        return new OutgoingMessage
        {
            Content = _content,
            Reply = _reply
        };
    }
}
