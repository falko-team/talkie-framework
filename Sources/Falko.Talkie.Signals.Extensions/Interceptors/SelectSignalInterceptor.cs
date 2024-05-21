using Talkie.Signals;

namespace Talkie.Interceptors;

public sealed class SelectSignalInterceptor(Func<Signal, CancellationToken, Signal> select) : ISignalInterceptor
{
    public InterceptionResult Intercept(Signal signal, CancellationToken cancellationToken)
    {
        return InterceptionResult.ContinueWith(select(signal, cancellationToken));
    }
}
