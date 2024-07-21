using Talkie.Handlers;

namespace Talkie.Pipelines.Handling;

public interface ISignalHandlingPipelineBuilder : IReadOnlySignalHandlingPipelineBuilder
{
    ISignalHandlingPipelineBuilder HandleAsync(ISignalHandler handler);
}
