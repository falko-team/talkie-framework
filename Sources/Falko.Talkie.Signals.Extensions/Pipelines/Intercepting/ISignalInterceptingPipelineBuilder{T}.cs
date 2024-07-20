using Talkie.Interceptors;
using Talkie.Piepelines2.Intercepting;
using Talkie.Signals;

namespace Talkie.Pipelines.Intercepting;

public interface ISignalInterceptingPipelineBuilder<out T> : ISignalInterceptingPipelineBuilder where T : Signal
{
    ISignalInterceptingPipelineBuilder<T> Intercept(ISignalInterceptorFactory<ISignalInterceptor<T>> interceptor);
}
