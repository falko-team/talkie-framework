using Talkie.Models.Identifiers;
using Talkie.Models.Messages.Contents;
using Talkie.Models.Messages.Features;
using Talkie.Sequences;

namespace Talkie.Models.Messages.Outgoing;

public sealed class OutgoingMessageBuilder : IOutgoingMessageBuilder
{
    private readonly Sequence<IMessageFeature> _features = [];

    public MessageContent Content { get; private set; }

    public GlobalMessageIdentifier? Reply { get; private set; }

    public IEnumerable<IMessageFeature> Features => _features;

    public IOutgoingMessageBuilder AddFeature(IMessageFeature feature)
    {
        _features.Add(feature);

        return this;
    }

    public IOutgoingMessageBuilder AddFeatures(IEnumerable<IMessageFeature> features)
    {
        _features.AddRange(features);

        return this;
    }

    public IOutgoingMessageBuilder SetReply(GlobalMessageIdentifier reply)
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
