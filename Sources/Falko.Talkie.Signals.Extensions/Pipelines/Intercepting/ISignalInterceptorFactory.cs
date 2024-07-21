using Talkie.Interceptors;

namespace Talkie.Pipelines.Intercepting;

public interface ISignalInterceptorFactory
{
    ISignalInterceptor Create();
}
