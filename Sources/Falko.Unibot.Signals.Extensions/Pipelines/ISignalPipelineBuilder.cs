using Falko.Unibot.Collections;
using Falko.Unibot.Handlers;
using Falko.Unibot.Interceptors;

namespace Falko.Unibot.Pipelines;

public interface ISignalPipelineBuilder
{
    ISignalPipeline Build();

    FrozenSequence<ISignalInterceptor> ToInterceptors();

    FrozenSequence<ISignalHandler> ToHandlers();
}
