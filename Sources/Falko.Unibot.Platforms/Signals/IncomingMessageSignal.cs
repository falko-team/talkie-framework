using Falko.Unibot.Models.Messages;

namespace Falko.Unibot.Signals;

public abstract record IncomingMessageSignal : Signal
{
    public abstract IIncomingMessage Message { get; }
}
