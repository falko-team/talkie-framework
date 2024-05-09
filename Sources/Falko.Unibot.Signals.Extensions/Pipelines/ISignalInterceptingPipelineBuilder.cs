using Falko.Unibot.Handlers;
using Falko.Unibot.Interceptors;

namespace Falko.Unibot.Pipelines;

public interface ISignalInterceptingPipelineBuilder : ISignalPipelineBuilder
{
    ISignalInterceptingPipelineBuilder Intercept(ISignalInterceptor interceptor);

    ISignalHandlingPipelineBuilder Handle(ISignalHandler handler);
}
