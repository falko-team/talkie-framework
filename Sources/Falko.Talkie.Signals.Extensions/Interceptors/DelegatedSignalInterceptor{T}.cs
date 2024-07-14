using Talkie.Signals;

namespace Talkie.Interceptors;

internal sealed class DelegatedSignalInterceptor<T>(Func<T, CancellationToken, InterceptionResult> intercept)
    : SignalInterceptor<T> where T : Signal
{
    public override InterceptionResult Intercept(T signal, CancellationToken cancellationToken)
    {
        return intercept(signal, cancellationToken);
    }
}
