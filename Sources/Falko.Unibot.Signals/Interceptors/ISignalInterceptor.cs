using Falko.Unibot.Signals;

namespace Falko.Unibot.Interceptors;

public interface ISignalInterceptor
{
    InterceptionResult Intercept(Signal signal, CancellationToken cancellationToken);
}
