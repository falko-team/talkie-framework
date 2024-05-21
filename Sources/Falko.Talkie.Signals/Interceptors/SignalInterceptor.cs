using Talkie.Signals;

namespace Talkie.Interceptors;

public abstract class SignalInterceptor<T> : ISignalInterceptor<T> where T : Signal
{
    public abstract InterceptionResult Intercept(T signal, CancellationToken cancellationToken);

    InterceptionResult ISignalInterceptor.Intercept(Signal signal, CancellationToken cancellationToken)
    {
        return signal is T castedSignal
            ? Intercept(castedSignal, cancellationToken)
            : InterceptionResult.Break();
    }
}
