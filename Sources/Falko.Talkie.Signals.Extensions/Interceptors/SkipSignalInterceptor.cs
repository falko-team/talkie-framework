using Talkie.Signals;

namespace Talkie.Interceptors;

internal sealed class SkipSignalInterceptor<T>(int count) : ISignalInterceptor<T> where T : Signal
{
    private readonly object _locker = new();

    private int _iterations;

    public InterceptionResult Intercept(Signal signal, CancellationToken cancellationToken)
    {
        lock (_locker)
        {
            if (_iterations == count)
            {
                return InterceptionResult.Continue();
            }

            ++_iterations;

            return InterceptionResult.Break();
        }
    }

    InterceptionResult ISignalInterceptor<T>.Intercept(T signal, CancellationToken cancellationToken)
    {
        return Intercept(signal, cancellationToken);
    }
}
