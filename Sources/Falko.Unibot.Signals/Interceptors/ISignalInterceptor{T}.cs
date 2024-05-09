using Falko.Unibot.Signals;

namespace Falko.Unibot.Interceptors;

public interface ISignalInterceptor<in T> : ISignalInterceptor where T : Signal
{
    InterceptionResult Intercept(T signal, CancellationToken cancellationToken);
}
