using Falko.Unibot.Models.Messages;

namespace Falko.Unibot.Signals;

public sealed record TelegramIncomingMessageSignal(TelegramIncomingMessage Message) : IncomingMessageSignal
{
    public override TelegramIncomingMessage Message { get; } = Message;
}
