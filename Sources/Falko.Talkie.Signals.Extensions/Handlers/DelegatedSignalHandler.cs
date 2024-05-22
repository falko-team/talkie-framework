namespace Talkie.Handlers;

public sealed class DelegatedSignalHandler(Func<ISignalContext, CancellationToken, ValueTask> handleAsync)
    : ISignalHandler
{
    public ValueTask HandleAsync(ISignalContext context, CancellationToken cancellationToken)
    {
        return handleAsync(context, cancellationToken);
    }
}
