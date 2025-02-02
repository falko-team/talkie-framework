using Talkie.Models.Identifiers;
using Talkie.Models.Messages.Attachments.Factories;
using Talkie.Models.Messages.Contents;
using Talkie.Models.Messages.Features;

namespace Talkie.Models.Messages.Outgoing;

public interface IOutgoingMessageBuilder
{
    MessageContent Content { get; }

    GlobalMessageIdentifier? Reply { get; }

    IEnumerable<IMessageFeature> Features { get; }

    IEnumerable<IMessageAttachmentFactory> Attachments { get; }

    IOutgoingMessageBuilder AddAttachment(IMessageAttachmentFactory attachment);

    IOutgoingMessageBuilder AddFeature(IMessageFeature feature);

    IOutgoingMessageBuilder AddFeatures(IEnumerable<IMessageFeature> features);

    IOutgoingMessageBuilder SetReply(GlobalMessageIdentifier reply);

    IOutgoingMessageBuilder SetContent(MessageContent content);

    IOutgoingMessage Build();
}
