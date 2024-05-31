using Talkie.Models.Messages;

namespace Talkie.Signals;

public sealed record TelegramIncomingMessageSignal(TelegramIncomingMessage Message) : IncomingMessageSignal
{
    public override TelegramIncomingMessage Message { get; } = Message;

    public override TelegramIncomingMessageSignal MutateMessage(Func<IIncomingMessageMutator, IIncomingMessageMutator> mutation)
    {
        return new TelegramIncomingMessageSignal(Message.Mutate(mutator => (TelegramIncomingMessageMutator)mutation(mutator)));
    }
}
