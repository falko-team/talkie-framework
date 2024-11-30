using Talkie.Handlers;
using Talkie.Interceptors;
using Talkie.Pipelines.Handling;
using Talkie.Sequences;

namespace Talkie.Pipelines.Intercepting;

public interface ISignalInterceptingPipelineProcessorFactory
{
    ISignalHandlingPipelineProcessor Create(FrozenSequence<ISignalInterceptor> interceptors);
}
