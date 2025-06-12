using Falko.Talkie.Signals;

namespace Falko.Talkie.Interceptors;

internal sealed class TakeSignalInterceptor<T>(int count) : SignalInterceptor<T> where T : Signal
{
    private volatile int _iterations = -1;

    public override InterceptionResult Intercept(T signal, CancellationToken cancellationToken)
    {
        for (;;)
        {
            var currentIterations = _iterations;

            if (currentIterations >= count) return InterceptionResult.Break();

            var nextIterations = currentIterations + 1;

            if (Interlocked.CompareExchange(ref _iterations, nextIterations, currentIterations) == currentIterations)
            {
                return nextIterations == count
                    ? InterceptionResult.Break()
                    : InterceptionResult.Continue();
            }
        }
    }
}
