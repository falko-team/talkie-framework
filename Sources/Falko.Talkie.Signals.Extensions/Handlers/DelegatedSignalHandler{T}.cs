using Falko.Talkie.Signals;

namespace Falko.Talkie.Handlers;

public sealed class DelegatedSignalHandler<T>(Func<ISignalContext<T>, CancellationToken, ValueTask> handleAsync)
    : SignalHandler<T> where T : Signal
{
    public override ValueTask HandleAsync(ISignalContext<T> context, CancellationToken cancellationToken)
    {
        return handleAsync(context, cancellationToken);
    }
}
