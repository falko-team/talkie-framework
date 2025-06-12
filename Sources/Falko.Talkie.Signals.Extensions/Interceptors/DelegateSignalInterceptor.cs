using Falko.Talkie.Signals;

namespace Falko.Talkie.Interceptors;

internal sealed class DelegateSignalInterceptor(Func<Signal, CancellationToken, InterceptionResult> intercept)
    : ISignalInterceptor
{
    public InterceptionResult Intercept(Signal signal, CancellationToken cancellationToken)
    {
        return intercept(signal, cancellationToken);
    }
}
