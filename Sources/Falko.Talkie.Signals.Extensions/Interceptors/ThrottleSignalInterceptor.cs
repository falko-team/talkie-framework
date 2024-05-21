using Talkie.Signals;

namespace Talkie.Interceptors;

public sealed class ThrottleSignalInterceptor<T>(TimeSpan delay) : ISignalInterceptor<T> where T : Signal
{
    private readonly object _locker = new();

    public InterceptionResult Intercept(Signal signal, CancellationToken cancellationToken)
    {
        lock (_locker)
        {
            try
            {
                Task.Delay(delay, cancellationToken).Wait(cancellationToken);

                return InterceptionResult.Continue();
            }
            catch
            {
                return InterceptionResult.Break();
            }
        }
    }

    InterceptionResult ISignalInterceptor<T>.Intercept(T signal, CancellationToken cancellationToken)
    {
        return Intercept(signal, cancellationToken);
    }
}
