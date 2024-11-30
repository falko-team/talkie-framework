using Talkie.Handlers;
using Talkie.Sequences;

namespace Talkie.Pipelines.Handling;

public interface ISignalHandlingPipelineProcessorFactory
{
    ISignalHandlingPipelineProcessor Create(FrozenSequence<ISignalHandler> handlers);
}
