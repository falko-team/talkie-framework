using Falko.Unibot.Flows;
using Falko.Unibot.Signals;

namespace Falko.Unibot.Handlers;

public interface ISignalHandler<in T> : ISignalHandler where T : Signal
{
    void Handle(ISignalContext<T> context, CancellationToken cancellationToken);
}
