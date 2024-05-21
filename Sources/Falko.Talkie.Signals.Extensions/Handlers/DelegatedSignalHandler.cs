namespace Falko.Talkie.Handlers;

public sealed class DelegatedSignalHandler(Action<ISignalContext, CancellationToken> handle) : ISignalHandler
{
    public void Handle(ISignalContext context, CancellationToken cancellationToken)
    {
        handle(context, cancellationToken);
    }
}
