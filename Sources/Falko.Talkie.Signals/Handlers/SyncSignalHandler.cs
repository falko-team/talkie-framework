namespace Falko.Talkie.Handlers;

public abstract class SyncSignalHandler : ISignalHandler
{
    public abstract void Handle(ISignalContext context, CancellationToken cancellationToken);

    ValueTask ISignalHandler.HandleAsync(ISignalContext context, CancellationToken cancellationToken)
    {
        Handle(context, cancellationToken);

        return ValueTask.CompletedTask;
    }
}
