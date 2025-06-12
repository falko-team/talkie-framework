using Falko.Talkie.Signals;

namespace Falko.Talkie.Interceptors;

internal sealed class SkipSignalInterceptor<T>(int count) : SignalInterceptor<T> where T : Signal
{
    private int _iterations = -1;

    public override InterceptionResult Intercept(T signal, CancellationToken cancellationToken)
    {
        for (;;)
        {
            var currentIterations = _iterations;

            if (currentIterations >= count) return InterceptionResult.Continue();

            var nextIterations = currentIterations + 1;

            if (Interlocked.CompareExchange(ref _iterations, nextIterations, currentIterations) == currentIterations)
            {
                return nextIterations == count
                    ? InterceptionResult.Continue()
                    : InterceptionResult.Break();
            }
        }
    }
}
