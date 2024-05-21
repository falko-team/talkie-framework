using Talkie.Handlers;

namespace Talkie.Pipelines;

public interface ISignalHandlingPipelineBuilder : ISignalPipelineBuilder
{
    ISignalHandlingPipelineBuilder Handle(ISignalHandler handler);
}
