using Falko.Talkie.Signals;

namespace Falko.Talkie.Pipelines.Intercepting;

public sealed class EmptySignalInterceptingPipeline : ISignalInterceptingPipeline
{
    public static readonly EmptySignalInterceptingPipeline Instance = new();

    private EmptySignalInterceptingPipeline() { }

    public bool TryTransfer(Signal incomingSignal, out Signal outgoingSignal, CancellationToken cancellationToken = default)
    {
        outgoingSignal = incomingSignal;
        return true;
    }
}
