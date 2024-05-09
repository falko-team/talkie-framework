using Falko.Unibot.Signals;

namespace Falko.Unibot.Handlers;

public sealed class DelegatedSignalHandler<T>(Action<ISignalContext<T>, CancellationToken> handle) : SignalHandler<T> where T : Signal
{
    public override void Handle(ISignalContext<T> context, CancellationToken cancellationToken)
    {
        handle(context, cancellationToken);
    }
}
