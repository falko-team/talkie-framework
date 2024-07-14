using Talkie.Collections;
using Talkie.Signals;

namespace Talkie.Interceptors;

internal sealed class MergedInterceptor(IEnumerable<ISignalInterceptor> interceptors,
    IEnumerable<ISignalInterceptor> mergedInterceptors) : ISignalInterceptor
{
    private readonly FrozenSequence<ISignalInterceptor> _interceptors = interceptors.ToFrozenSequence();

    private readonly FrozenSequence<ISignalInterceptor> _mergedInterceptors = mergedInterceptors.ToFrozenSequence();

    public InterceptionResult Intercept(Signal signal, CancellationToken cancellationToken)
    {
        var currentSignal = signal;

        var lastResult = InterceptionResult.Continue();

        foreach (var interceptor in _interceptors)
        {
            lastResult = interceptor.Intercept(currentSignal, cancellationToken);

            if (lastResult.CanContinue is false)
            {
                break;
            }

            if (lastResult.ReplacedSignal is not null)
            {
                currentSignal = lastResult.ReplacedSignal;
            }
        }

        if (lastResult.CanContinue)
        {
            return lastResult;
        }

        currentSignal = signal;
        lastResult = InterceptionResult.Continue();

        foreach (var interceptor in _mergedInterceptors)
        {
            lastResult = interceptor.Intercept(currentSignal, cancellationToken);

            if (lastResult.CanContinue is false)
            {
                break;
            }

            if (lastResult.ReplacedSignal is not null)
            {
                currentSignal = lastResult.ReplacedSignal;
            }
        }

        return lastResult;
    }
}
