using Talkie.Flows;
using Talkie.Signals;

namespace Talkie.Pipelines;

public interface ISignalPipeline
{
    void Transfer(ISignalFlow flow, Signal signal, CancellationToken cancellationToken = default);
}
