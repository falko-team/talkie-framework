using Falko.Talkie.Models.Messages;

namespace Falko.Talkie.Signals;

public abstract record IncomingMessageSignal : Signal
{
    public abstract IIncomingMessage Message { get; }
}
