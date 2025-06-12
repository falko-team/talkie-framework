using Falko.Talkie.Signals;

namespace Falko.Talkie.Interceptors;

internal sealed class DelegateSignalInterceptor<T>(Func<T, CancellationToken, InterceptionResult> intercept)
    : SignalInterceptor<T> where T : Signal
{
    public override InterceptionResult Intercept(T signal, CancellationToken cancellationToken)
    {
        return intercept(signal, cancellationToken);
    }
}
