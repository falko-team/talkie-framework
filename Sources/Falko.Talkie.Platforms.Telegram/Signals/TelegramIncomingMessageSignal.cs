using Falko.Talkie.Models.Messages;

namespace Falko.Talkie.Signals;

public sealed record TelegramIncomingMessageSignal(TelegramIncomingMessage Message) : IncomingMessageSignal
{
    public override TelegramIncomingMessage Message { get; } = Message;
}
