using Talkie.Flows;
using Talkie.Signals;

namespace Talkie.Pipelines.Handling;

public interface ISignalHandlingPipeline
{
    ValueTask TransferAsync(ISignalFlow flow, Signal signal, CancellationToken cancellationToken = default);
}
