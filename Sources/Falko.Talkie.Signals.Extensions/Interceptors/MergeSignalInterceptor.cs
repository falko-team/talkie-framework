using Falko.Talkie.Pipelines.Intercepting;
using Falko.Talkie.Signals;

namespace Falko.Talkie.Interceptors;

internal sealed class MergeSignalInterceptor<T>
(
    ISignalInterceptingPipeline targetPipeline,
    ISignalInterceptingPipeline mergePipeline
) : ISignalInterceptor<T> where T : Signal
{
    public InterceptionResult Intercept(Signal signal, CancellationToken cancellationToken)
    {
        if (targetPipeline.TryTransfer(signal, out var targetSignal, cancellationToken))
        {
            return targetSignal;
        }

        if (mergePipeline.TryTransfer(signal, out var mergeSignal, cancellationToken))
        {
            return mergeSignal;
        }

        return InterceptionResult.Break();
    }

    InterceptionResult ISignalInterceptor<T>.Intercept(T signal, CancellationToken cancellationToken)
    {
        return Intercept(signal, cancellationToken);
    }
}
