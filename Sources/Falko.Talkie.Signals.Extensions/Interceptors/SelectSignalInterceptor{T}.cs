using Talkie.Signals;

namespace Talkie.Interceptors;

public sealed class SelectSignalInterceptor<TFrom, TTo>(Func<TFrom, CancellationToken, TTo> select) : SignalInterceptor<TFrom>
    where TFrom : Signal
    where TTo : Signal
{
    public override InterceptionResult Intercept(TFrom signal, CancellationToken cancellationToken)
    {
        return InterceptionResult.ContinueWith(select(signal, cancellationToken));
    }
}
