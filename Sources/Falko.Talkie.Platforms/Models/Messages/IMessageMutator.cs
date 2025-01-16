using Talkie.Models.Messages.Contents;
using Talkie.Models.Messages.Features;

namespace Talkie.Models.Messages;

public interface IMessageMutator<out TMutator, out TMessage>
    where TMutator : IMessageMutator<TMutator, TMessage>
    where TMessage : IMessage
{
    TMutator MutateFeatures<TFeature>
    (
        Func<IEnumerable<IMessageFeature>, IEnumerable<IMessageFeature>> featuresMutationFactory
    ) where TFeature : IMessageFeature;

    TMutator MutateContent(Func<MessageContent, MessageContent> contentMutationFactory);

    TMessage Mutate();
}
