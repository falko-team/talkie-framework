using Falko.Talkie.Signals;

namespace Falko.Talkie.Interceptors;

public interface ISignalInterceptor
{
    InterceptionResult Intercept(Signal signal, CancellationToken cancellationToken);
}
