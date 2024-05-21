using Talkie.Collections;
using Talkie.Handlers;
using Talkie.Interceptors;

namespace Talkie.Pipelines;

public interface ISignalPipelineBuilder
{
    ISignalPipeline Build();

    FrozenSequence<ISignalInterceptor> ToInterceptors();

    FrozenSequence<ISignalHandler> ToHandlers();
}
