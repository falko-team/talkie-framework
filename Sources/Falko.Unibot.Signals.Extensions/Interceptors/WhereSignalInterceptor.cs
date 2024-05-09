using Falko.Unibot.Signals;

namespace Falko.Unibot.Interceptors;

public sealed class WhereSignalInterceptor(Func<Signal, CancellationToken, bool> where) : ISignalInterceptor
{
    public InterceptionResult Intercept(Signal signal, CancellationToken cancellationToken)
    {
        return where(signal, cancellationToken);
    }
}
