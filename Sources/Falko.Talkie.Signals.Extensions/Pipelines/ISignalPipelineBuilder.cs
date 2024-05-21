using Falko.Talkie.Collections;
using Falko.Talkie.Handlers;
using Falko.Talkie.Interceptors;

namespace Falko.Talkie.Pipelines;

public interface ISignalPipelineBuilder
{
    ISignalPipeline Build();

    FrozenSequence<ISignalInterceptor> ToInterceptors();

    FrozenSequence<ISignalHandler> ToHandlers();
}
