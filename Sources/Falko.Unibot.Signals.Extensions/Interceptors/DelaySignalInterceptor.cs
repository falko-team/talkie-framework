using Falko.Unibot.Signals;

namespace Falko.Unibot.Interceptors;

public sealed class DelaySignalInterceptor<T>(TimeSpan delay) : ISignalInterceptor<T> where T : Signal
{
    public InterceptionResult Intercept(Signal signal, CancellationToken cancellationToken)
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

    InterceptionResult ISignalInterceptor<T>.Intercept(T signal, CancellationToken cancellationToken)
    {
        return Intercept(signal, cancellationToken);
    }
}
