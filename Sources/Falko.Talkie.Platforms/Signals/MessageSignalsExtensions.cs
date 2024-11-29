using Talkie.Models.Messages.Incoming;

namespace Talkie.Signals;

public static partial class MessageSignalsExtensions
{
    public static MessagePublishedSignal MutateMessage
    (
        this MessagePublishedSignal signal,
        Func<IIncomingMessageMutator, IIncomingMessageMutator> messageMutationFactory
    )
    {
        return messageMutationFactory(signal.Message.ToMutator()).Mutate().ToMessagePublishedSignal();
    }

    public static MessageExchangedSignal MutateMessage
    (
        this MessageExchangedSignal signal,
        Func<IIncomingMessageMutator, IIncomingMessageMutator> messageMutationFactory
    )
    {
        return messageMutationFactory(signal.Message.ToMutator()).Mutate().ToMessageExchangedSignal();
    }
}
