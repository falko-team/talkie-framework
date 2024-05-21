using Falko.Talkie.Flows;
using Falko.Talkie.Signals;

namespace Falko.Talkie.Pipelines;

public interface ISignalPipeline
{
    void Transfer(ISignalFlow flow, Signal signal, CancellationToken cancellationToken = default);
}
