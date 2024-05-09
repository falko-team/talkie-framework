using Falko.Unibot.Pipelines;
using Falko.Unibot.Signals;

namespace Falko.Unibot.Flows;

public interface ISignalFlow : IDisposable
{
    Subscription Subscribe(ISignalPipeline pipeline);

    Task PublishAsync(Signal signal, CancellationToken cancellationToken = default);
}
