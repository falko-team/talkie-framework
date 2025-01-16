using Talkie.Models.Identifiers;
using Talkie.Models.Messages.Contents;
using Talkie.Models.Messages.Features;

namespace Talkie.Models.Messages.Outgoing;

public interface IOutgoingMessageBuilder
{
    MessageContent Content { get; }

    GlobalMessageIdentifier? Reply { get; }

    IEnumerable<IMessageFeature> Features { get; }

    IOutgoingMessageBuilder AddFeature(IMessageFeature feature);

    IOutgoingMessageBuilder AddFeatures(IEnumerable<IMessageFeature> features);

    IOutgoingMessageBuilder SetReply(GlobalMessageIdentifier reply);

    IOutgoingMessageBuilder SetContent(MessageContent content);

    IOutgoingMessage Build();
}
