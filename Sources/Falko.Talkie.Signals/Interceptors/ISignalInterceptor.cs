using Talkie.Signals;

namespace Talkie.Interceptors;

public interface ISignalInterceptor
{
    InterceptionResult Intercept(Signal signal, CancellationToken cancellationToken);
}
