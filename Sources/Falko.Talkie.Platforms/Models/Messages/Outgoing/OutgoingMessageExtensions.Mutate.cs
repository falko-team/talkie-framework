namespace Talkie.Models.Messages.Outgoing;

public static partial class OutgoingMessageExtensions
{
    public static IOutgoingMessage Mutate
    (
        this IOutgoingMessage message,
        Func<IOutgoingMessageMutator, IOutgoingMessageMutator> mutationFactory
    )
    {
        return mutationFactory(message.ToMutator()).Mutate();
    }
}
