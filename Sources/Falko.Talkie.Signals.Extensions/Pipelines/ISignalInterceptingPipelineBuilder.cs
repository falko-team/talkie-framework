using Falko.Talkie.Handlers;
using Falko.Talkie.Interceptors;

namespace Falko.Talkie.Pipelines;

public interface ISignalInterceptingPipelineBuilder : ISignalPipelineBuilder
{
    ISignalInterceptingPipelineBuilder Intercept(ISignalInterceptor interceptor);

    ISignalHandlingPipelineBuilder Handle(ISignalHandler handler);
}
