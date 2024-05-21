using Talkie.Handlers;
using Talkie.Interceptors;

namespace Talkie.Pipelines;

public interface ISignalInterceptingPipelineBuilder : ISignalPipelineBuilder
{
    ISignalInterceptingPipelineBuilder Intercept(ISignalInterceptor interceptor);

    ISignalHandlingPipelineBuilder Handle(ISignalHandler handler);
}
