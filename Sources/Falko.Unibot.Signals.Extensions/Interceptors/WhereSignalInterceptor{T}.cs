using Falko.Unibot.Signals;

namespace Falko.Unibot.Interceptors;

public sealed class WhereSignalInterceptor<T>(Func<T, CancellationToken, bool> where) : SignalInterceptor<T> where T : Signal
{
    public override InterceptionResult Intercept(T signal, CancellationToken cancellationToken)
    {
        return where(signal, cancellationToken);
    }
}
