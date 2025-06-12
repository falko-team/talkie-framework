using Falko.Talkie.Signals;

namespace Falko.Talkie.Pipelines.Intercepting;

public interface ISignalInterceptingPipelineBuilder<out T> : IReadOnlySignalInterceptingPipelineBuilder where T : Signal
{
    ISignalInterceptingPipelineBuilder<T> Intercept(ISignalInterceptorFactory<T> interceptor);
}
