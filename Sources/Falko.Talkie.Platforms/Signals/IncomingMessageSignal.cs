using Talkie.Models.Messages;

namespace Talkie.Signals;

public abstract record IncomingMessageSignal : Signal
{
    public abstract IIncomingMessage Message { get; }
}
