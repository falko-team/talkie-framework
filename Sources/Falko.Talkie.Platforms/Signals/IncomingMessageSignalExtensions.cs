using Talkie.Models.Messages;

namespace Talkie.Signals;

public static class IncomingMessageSignalExtensions
{
    public static IncomingMessageSignal MutateMessage(this IncomingMessageSignal signal,
        Func<IIncomingMessageMutator, IIncomingMessageMutator> messageMutationFactory)
    {
        return messageMutationFactory(signal.Message.ToMutator()).Mutate().ToSignal();
    }
}
