using Falko.Talkie.Signals;

namespace Falko.Talkie.Handlers;

public abstract class SyncSignalHandler<T> : ISignalHandler<T> where T : Signal
{
    public abstract void Handle(ISignalContext<T> context, CancellationToken cancellationToken);

    ValueTask ISignalHandler.HandleAsync(ISignalContext context, CancellationToken cancellationToken)
    {
        if (context.Signal is T castedSignal)
        {
            Handle(new SignalContext<T>(context.Flow, castedSignal), cancellationToken);
        }

        return ValueTask.CompletedTask;
    }

    ValueTask ISignalHandler<T>.HandleAsync(ISignalContext<T> context, CancellationToken cancellationToken)
    {
        Handle(context, cancellationToken);

        return ValueTask.CompletedTask;
    }
}
