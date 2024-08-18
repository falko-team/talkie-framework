using Talkie.Models.Messages.Incoming;

namespace Talkie.Signals;

public static partial class IncomingMessageSignalExtensions
{
    public static IncomingMessageSignal MutateMessage(this IncomingMessageSignal signal,
        Func<IIncomingMessageMutator, IIncomingMessageMutator> messageMutationFactory)
    {
        return messageMutationFactory(signal.Message.ToMutator()).Mutate().ToSignal();
    }
}
