using Falko.Talkie.Models.Identifiers;
using Falko.Talkie.Models.Messages.Contents;
using Falko.Talkie.Models.Messages.Features;

namespace Falko.Talkie.Models.Messages.Outgoing;

public sealed class OutgoingMessageMutator : IOutgoingMessageMutator
{
    private readonly OutgoingMessage _message;

    private MessageContent _content;

    private GlobalMessageIdentifier? _reply;

    private IEnumerable<IMessageFeature> _features;

    internal OutgoingMessageMutator(OutgoingMessage message)
    {
        _message = message;
        _content = message.Content;
        _reply = message.Reply;
        _features = message.Features;
    }

    public IOutgoingMessageMutator MutateReply(Func<GlobalMessageIdentifier?, GlobalMessageIdentifier?> replyMutationFactory)
    {
        _reply = replyMutationFactory(_reply);

        return this;
    }

    public IOutgoingMessageMutator MutateFeatures<TFeature>
    (
        Func<IEnumerable<IMessageFeature>, IEnumerable<IMessageFeature>> featuresMutationFactory
    ) where TFeature : IMessageFeature
    {
        _features = featuresMutationFactory(_features);

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
            Reply = _reply,
            Features = _features
        };
    }
}
