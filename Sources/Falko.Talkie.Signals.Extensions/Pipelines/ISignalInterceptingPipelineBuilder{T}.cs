using Falko.Talkie.Handlers;
using Falko.Talkie.Interceptors;
using Falko.Talkie.Signals;

namespace Falko.Talkie.Pipelines;

public interface ISignalInterceptingPipelineBuilder<out T> : ISignalPipelineBuilder where T : Signal
{
    ISignalInterceptingPipelineBuilder<T> Intercept(ISignalInterceptor<T> interceptor);

    ISignalHandlingPipelineBuilder<T> Handle(ISignalHandler<T> handler);
}
