using Falko.Unibot.Signals;

namespace Falko.Unibot.Handlers;

public abstract class SignalHandler<T> : ISignalHandler<T> where T : Signal
{
    public abstract void Handle(ISignalContext<T> context, CancellationToken cancellationToken);

    void ISignalHandler.Handle(ISignalContext context, CancellationToken cancellationToken)
    {
        if (context.Signal is not T castedSignal) return;

        Handle(new SignalContext<T>(context.Flow, castedSignal), cancellationToken);
    }
}
