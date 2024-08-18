using Talkie.Models.Messages.Outgoing;

namespace Talkie.Signals;

public static partial class OutgoingMessageSignalExtensions
{
    public static OutgoingMessageSignal MutateMessage(this OutgoingMessageSignal signal,
        Func<IOutgoingMessageMutator, IOutgoingMessageMutator> messageMutationFactory)
    {
        return messageMutationFactory(signal.Message.ToMutator()).Mutate().ToSignal();
    }
}
