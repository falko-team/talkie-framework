using Talkie.Signals;

namespace Talkie.Interceptors;

internal sealed class WhereSignalInterceptor(Func<Signal, CancellationToken, bool> where) : ISignalInterceptor
{
    public InterceptionResult Intercept(Signal signal, CancellationToken cancellationToken)
    {
        return where(signal, cancellationToken);
    }
}
