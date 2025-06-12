using Falko.Talkie.Models.Identifiers;
using Falko.Talkie.Models.Messages.Attachments.Factories;
using Falko.Talkie.Models.Messages.Contents;
using Falko.Talkie.Models.Messages.Features;

namespace Falko.Talkie.Models.Messages.Outgoing;

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
