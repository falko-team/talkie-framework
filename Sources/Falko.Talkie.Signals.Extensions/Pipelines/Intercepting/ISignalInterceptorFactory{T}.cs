using Talkie.Interceptors;

namespace Talkie.Piepelines2.Intercepting;

public interface ISignalInterceptorFactory<out T> : ISignalInterceptorFactory where T : ISignalInterceptor
{
    new T Create();
}
