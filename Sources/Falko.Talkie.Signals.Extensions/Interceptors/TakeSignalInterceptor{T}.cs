using Talkie.Signals;

namespace Talkie.Interceptors;

internal sealed class TakeSignalInterceptor<T>(int count) : SignalInterceptor<T> where T : Signal
{
    private readonly object _locker = new();

    private int _iterations;

    public override InterceptionResult Intercept(T signal, CancellationToken cancellationToken)
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
}
