using Talkie.Handlers;
using Talkie.Interceptors;
using Talkie.Signals;

namespace Talkie.Pipelines;

public interface ISignalInterceptingPipelineBuilder<out T> : ISignalPipelineBuilder where T : Signal
{
    ISignalInterceptingPipelineBuilder<T> Intercept(ISignalInterceptor<T> interceptor);

    ISignalHandlingPipelineBuilder<T> Handle(ISignalHandler<T> handler);
}
