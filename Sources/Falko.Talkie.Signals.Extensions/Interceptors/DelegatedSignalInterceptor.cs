using Talkie.Signals;

namespace Talkie.Interceptors;

public sealed class DelegatedSignalInterceptor(Func<Signal, CancellationToken, InterceptionResult> intercept)
    : ISignalInterceptor
{
    public InterceptionResult Intercept(Signal signal, CancellationToken cancellationToken)
    {
        return intercept(signal, cancellationToken);
    }
}
