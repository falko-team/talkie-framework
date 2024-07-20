using Talkie.Signals;

namespace Talkie.Pipelines.Intercepting;

public interface ISignalInterceptingPipeline
{
    bool TryTransfer(Signal incomingSignal, out Signal outgoingSignal, CancellationToken cancellationToken = default);
}
