namespace Talkie.Models.Messages;

public interface IMessageMutator<out TMutator, out TMessage>
    where TMutator : IMessageMutator<TMutator, TMessage>
    where TMessage : IMessage
{
    TMutator MutateText(Func<string?, string?> textMutationFactory);

    TMessage Mutate();
}
