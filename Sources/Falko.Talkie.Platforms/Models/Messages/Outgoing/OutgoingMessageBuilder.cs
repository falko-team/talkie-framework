using Falko.Talkie.Models.Identifiers;
using Falko.Talkie.Models.Messages.Attachments.Factories;
using Falko.Talkie.Models.Messages.Contents;
using Falko.Talkie.Models.Messages.Features;
using Falko.Talkie.Sequences;

namespace Falko.Talkie.Models.Messages.Outgoing;

public sealed class OutgoingMessageBuilder : IOutgoingMessageBuilder
{
    private readonly Sequence<IMessageFeature> _features = [];

    private readonly Sequence<IMessageAttachmentFactory> _attachments = [];

    public MessageContent Content { get; private set; } = MessageContent.Empty;

    public GlobalMessageIdentifier? Reply { get; private set; }

    public IEnumerable<IMessageFeature> Features => _features;

    public IEnumerable<IMessageAttachmentFactory> Attachments => _attachments;

    public IOutgoingMessageBuilder AddAttachment(IMessageAttachmentFactory attachment)
    {
        _attachments.Add(attachment);

        return this;
    }

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
        if (Content.IsEmpty && _attachments.Any() is false)
        {
            return OutgoingMessage.Empty;
        }

        return new OutgoingMessage
        {
            Content = Content,
            Attachments = _attachments,
            Features = _features,
            Reply = Reply
        };
    }
}
