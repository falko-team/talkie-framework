using Talkie.Models.Messages.Contents;

namespace Talkie.Models.Messages.Incoming;

public sealed class TelegramIncomingMessageMutator : IIncomingMessageMutator
{
    private readonly TelegramIncomingMessage _message;

    private MessageContent _content;

    private TelegramIncomingMessage? _reply;

    internal TelegramIncomingMessageMutator(TelegramIncomingMessage message)
    {
        _message = message;
        _content = message.Content;
        _reply = message.Reply;
    }

    public TelegramIncomingMessageMutator MutateContent(Func<MessageContent, MessageContent> contentMutationFactory)
    {
        _content = contentMutationFactory(_content);

        return this;
    }

    IIncomingMessageMutator IMessageMutator<IIncomingMessageMutator, IIncomingMessage>.MutateContent(Func<MessageContent, MessageContent> contentMutationFactory)
    {
        return MutateContent(contentMutationFactory);
    }

    public TelegramIncomingMessageMutator MutateReply(Func<TelegramIncomingMessage?, TelegramIncomingMessage?> replyMutationFactory)
    {
        _reply = replyMutationFactory(_reply);

        return this;
    }

    IIncomingMessageMutator IIncomingMessageMutator.MutateReply(Func<IIncomingMessage?, IIncomingMessage?> replyMutationFactory)
    {
        return MutateReply(replyMessage => (TelegramIncomingMessage?)replyMutationFactory(replyMessage));
    }

    public TelegramIncomingMessage Mutate()
    {
        return _message with
        {
            Content = _content,
            Reply = _reply
        };
    }

    IIncomingMessage IMessageMutator<IIncomingMessageMutator, IIncomingMessage>.Mutate()
    {
        return Mutate();
    }
}
