using Talkie.Models.Messages;

namespace Talkie.Signals;

public sealed record TelegramIncomingMessageSignal(TelegramIncomingMessage Message) : IncomingMessageSignal
{
    public override TelegramIncomingMessage Message { get; } = Message;

    public override IncomingMessageSignal Mutate(Func<IIncomingMessageMutator, IIncomingMessageMutator> mutation)
    {
        return new TelegramIncomingMessageSignal(Message.Mutate(mutation));
    }
}
