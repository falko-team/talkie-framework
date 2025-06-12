using Falko.Talkie.Interceptors;

namespace Falko.Talkie.Pipelines.Intercepting;

public interface ISignalInterceptorFactory
{
    ISignalInterceptor Create();
}
