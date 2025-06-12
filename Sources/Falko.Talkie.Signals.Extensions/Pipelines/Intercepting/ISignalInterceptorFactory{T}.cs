using Falko.Talkie.Interceptors;
using Falko.Talkie.Signals;

namespace Falko.Talkie.Pipelines.Intercepting;

public interface ISignalInterceptorFactory<in T> : ISignalInterceptorFactory where T : Signal
{
    new ISignalInterceptor<T> Create();
}
