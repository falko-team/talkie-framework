using Talkie.Collections;
using Talkie.Interceptors;
using Talkie.Signals;

namespace Talkie.Pipelines.Intercepting;

public sealed class SignalInterceptingPipeline(IEnumerable<ISignalInterceptor> interceptors) : ISignalInterceptingPipeline
{
    private readonly FrozenSequence<ISignalInterceptor> _interceptorsSequence = interceptors.ToFrozenSequence();

    public bool TryTransfer(Signal incomingSignal, out Signal outgoingSignal, CancellationToken cancellationToken = default)
    {
        outgoingSignal = incomingSignal;

        foreach (var interceptor in _interceptorsSequence)
        {
            var result = interceptor.Intercept(outgoingSignal, cancellationToken);

            if (result.CanContinue is false)
            {
                return false;
            }

            if (result.ReplacedSignal is not null)
            {
                outgoingSignal = result.ReplacedSignal;
            }
        }

        return true;
    }
}
