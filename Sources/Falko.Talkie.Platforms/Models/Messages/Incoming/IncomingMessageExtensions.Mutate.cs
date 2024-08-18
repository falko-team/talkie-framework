namespace Talkie.Models.Messages.Incoming;

public static partial class IncomingMessageExtensions
{
    public static IIncomingMessage Mutate(this IIncomingMessage message,
        Func<IIncomingMessageMutator, IIncomingMessageMutator> mutationFactory)
    {
        return mutationFactory(message.ToMutator()).Mutate();
    }
}
