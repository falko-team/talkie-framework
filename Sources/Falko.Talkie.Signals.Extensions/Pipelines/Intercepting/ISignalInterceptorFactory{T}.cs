using Talkie.Interceptors;
using Talkie.Signals;

namespace Talkie.Pipelines.Intercepting;

public interface ISignalInterceptorFactory<in T> : ISignalInterceptorFactory where T : Signal
{
    new ISignalInterceptor<T> Create();
}
