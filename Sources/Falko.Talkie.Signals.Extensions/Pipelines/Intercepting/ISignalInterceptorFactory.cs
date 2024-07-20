using Talkie.Interceptors;

namespace Talkie.Piepelines2.Intercepting;

public interface ISignalInterceptorFactory
{
    ISignalInterceptor Create();
}
