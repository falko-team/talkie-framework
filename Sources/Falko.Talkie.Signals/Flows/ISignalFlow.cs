using Falko.Talkie.Pipelines;
using Falko.Talkie.Signals;

namespace Falko.Talkie.Flows;

public interface ISignalFlow : IDisposable
{
    Subscription Subscribe(ISignalPipeline pipeline);

    Task PublishAsync(Signal signal, CancellationToken cancellationToken = default);
}
