using Talkie.Pipelines.Handling;
using Talkie.Signals;

namespace Talkie.Flows;

public interface ISignalFlow : IDisposable
{
    Subscription Subscribe(ISignalHandlingPipeline handlingPipeline);

    Task PublishAsync(Signal signal, CancellationToken cancellationToken = default);
}
