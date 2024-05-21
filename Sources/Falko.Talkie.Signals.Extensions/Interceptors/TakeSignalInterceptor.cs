using Talkie.Signals;

namespace Talkie.Interceptors;

public sealed class TakeSignalInterceptor<T>(int count) : ISignalInterceptor<T> where T : Signal
{
    private readonly object _locker = new();

    private int _iterations;

    public InterceptionResult Intercept(Signal signal, CancellationToken cancellationToken)
    {
        lock (_locker)
        {
            if (_iterations == count)
            {
                return InterceptionResult.Break();
            }

            ++_iterations;

            return InterceptionResult.Continue();
        }
    }

    InterceptionResult ISignalInterceptor<T>.Intercept(T signal, CancellationToken cancellationToken)
    {
        return Intercept(signal, cancellationToken);
    }
}
