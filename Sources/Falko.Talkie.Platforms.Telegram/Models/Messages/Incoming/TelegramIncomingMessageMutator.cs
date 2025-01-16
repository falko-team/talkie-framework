using Talkie.Models.Messages.Contents;
using Talkie.Models.Messages.Features;

namespace Talkie.Models.Messages.Incoming;

public sealed class TelegramIncomingMessageMutator : IIncomingMessageMutator
{
    private readonly TelegramIncomingMessage _message;

    private MessageContent _content;

    private TelegramIncomingMessage? _reply;

    private IEnumerable<IMessageFeature> _features;

    internal TelegramIncomingMessageMutator(TelegramIncomingMessage message)
    {
        _message = message;
        _content = message.Content;
        _reply = message.Reply;
        _features = message.Features;
    }

    public IIncomingMessageMutator MutateFeatures<TFeature>
    (
        Func<IEnumerable<IMessageFeature>, IEnumerable<IMessageFeature>> featuresMutationFactory
    ) where TFeature : IMessageFeature
    {
        _features = featuresMutationFactory(_features);

        return this;
    }

    public TelegramIncomingMessageMutator MutateContent(Func<MessageContent, MessageContent> contentMutationFactory)
    {
        _content = contentMutationFactory(_content);

        return this;
    }

    IIncomingMessageMutator IMessageMutator<IIncomingMessageMutator, IIncomingMessage>.MutateContent
    (
        Func<MessageContent, MessageContent> contentMutationFactory
    )
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
            Reply = _reply,
            Features = _features
        };
    }

    IIncomingMessage IMessageMutator<IIncomingMessageMutator, IIncomingMessage>.Mutate()
    {
        return Mutate();
    }
}
