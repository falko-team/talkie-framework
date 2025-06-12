using Falko.Talkie.Pipelines.Handling;
using Falko.Talkie.Signals;

namespace Falko.Talkie.Flows;

public interface ISignalFlow : IDisposable
{
    Subscription Subscribe(ISignalHandlingPipeline handlingPipeline);

    Task PublishAsync(Signal signal, CancellationToken cancellationToken = default);
}
