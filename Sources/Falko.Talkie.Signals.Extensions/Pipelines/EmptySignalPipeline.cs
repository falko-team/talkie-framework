using Talkie.Flows;
using Talkie.Signals;

namespace Talkie.Pipelines;

public sealed class EmptySignalPipeline : ISignalPipeline
{
    public static readonly EmptySignalPipeline Instance = new();

    private EmptySignalPipeline() { }

    public void Transfer(ISignalFlow flow, Signal signal, CancellationToken cancellationToken = default) { }
}
