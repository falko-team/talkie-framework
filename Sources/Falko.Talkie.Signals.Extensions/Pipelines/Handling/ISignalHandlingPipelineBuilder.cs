using Falko.Talkie.Handlers;

namespace Falko.Talkie.Pipelines.Handling;

public interface ISignalHandlingPipelineBuilder : IReadOnlySignalHandlingPipelineBuilder
{
    ISignalHandlingPipelineBuilder HandleAsync(ISignalHandler handler);
}
