using Falko.Unibot.Flows;
using Falko.Unibot.Signals;

namespace Falko.Unibot.Pipelines;

public interface ISignalPipeline
{
    void Transfer(ISignalFlow flow, Signal signal, CancellationToken cancellationToken = default);
}
