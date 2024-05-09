using Falko.Unibot.Handlers;
using Falko.Unibot.Interceptors;
using Falko.Unibot.Signals;

namespace Falko.Unibot.Pipelines;

public interface ISignalInterceptingPipelineBuilder<out T> : ISignalPipelineBuilder where T : Signal
{
    ISignalInterceptingPipelineBuilder<T> Intercept(ISignalInterceptor<T> interceptor);

    ISignalHandlingPipelineBuilder<T> Handle(ISignalHandler<T> handler);
}
