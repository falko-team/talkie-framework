using Falko.Talkie.Signals;

namespace Falko.Talkie.Interceptors;

internal sealed class WhereSignalInterceptor<T>(Func<T, CancellationToken, bool> where)
    : SignalInterceptor<T> where T : Signal
{
    public override InterceptionResult Intercept(T signal, CancellationToken cancellationToken)
    {
        return where(signal, cancellationToken);
    }
}
