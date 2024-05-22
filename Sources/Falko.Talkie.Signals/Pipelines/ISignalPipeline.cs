using Talkie.Flows;
using Talkie.Signals;

namespace Talkie.Pipelines;

public interface ISignalPipeline
{
    ValueTask TransferAsync(ISignalFlow flow, Signal signal, CancellationToken cancellationToken = default);
}
