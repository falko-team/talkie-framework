using Falko.Unibot.Flows;
using Falko.Unibot.Signals;

namespace Falko.Unibot.Pipelines;

public sealed class EmptySignalPipeline : ISignalPipeline
{
    public static readonly EmptySignalPipeline Instance = new();

    private EmptySignalPipeline() { }

    public void Transfer(ISignalFlow flow, Signal signal, CancellationToken cancellationToken = default) { }
}
