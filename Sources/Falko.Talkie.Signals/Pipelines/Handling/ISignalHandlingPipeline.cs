using Falko.Talkie.Flows;
using Falko.Talkie.Signals;

namespace Falko.Talkie.Pipelines.Handling;

public interface ISignalHandlingPipeline
{
    ValueTask TransferAsync(ISignalFlow flow, Signal signal, CancellationToken cancellationToken = default);
}
