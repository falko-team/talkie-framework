using Talkie.Pipelines;
using Talkie.Signals;

namespace Talkie.Flows;

public interface ISignalFlow : IDisposable
{
    Subscription Subscribe(ISignalPipeline pipeline);

    Task PublishAsync(Signal signal, CancellationToken cancellationToken = default);
}
