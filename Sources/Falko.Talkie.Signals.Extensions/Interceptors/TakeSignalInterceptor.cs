using Talkie.Signals;

namespace Talkie.Interceptors;

internal sealed class TakeSignalInterceptor(int count) : ISignalInterceptor
{
    private volatile int _iterations = -1;

    public InterceptionResult Intercept(Signal signal, CancellationToken cancellationToken)
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
