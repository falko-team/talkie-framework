using Talkie.Signals;

namespace Talkie.Interceptors;

internal sealed class DelegatedSignalInterceptor(Func<Signal, CancellationToken, InterceptionResult> intercept)
    : ISignalInterceptor
{
    public InterceptionResult Intercept(Signal signal, CancellationToken cancellationToken)
    {
        return intercept(signal, cancellationToken);
    }
}
