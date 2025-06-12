using Falko.Talkie.Signals;

namespace Falko.Talkie.Handlers;

public abstract class SignalHandler<T> : ISignalHandler<T> where T : Signal
{
    public abstract ValueTask HandleAsync(ISignalContext<T> context, CancellationToken cancellationToken);

    ValueTask ISignalHandler.HandleAsync(ISignalContext context, CancellationToken cancellationToken)
    {
        return context.Signal is T castedSignal
            ? HandleAsync(new SignalContext<T>(context.Flow, castedSignal), cancellationToken)
            : ValueTask.CompletedTask;
    }
}
