using Talkie.Models.Messages.Contents;

namespace Talkie.Models.Messages;

public interface IMessageMutator<out TMutator, out TMessage>
    where TMutator : IMessageMutator<TMutator, TMessage>
    where TMessage : IMessage
{
    TMutator MutateContent(Func<MessageContent, MessageContent> contentMutationFactory);

    TMessage Mutate();
}
