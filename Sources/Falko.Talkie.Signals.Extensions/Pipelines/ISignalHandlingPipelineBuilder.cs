using Falko.Talkie.Handlers;

namespace Falko.Talkie.Pipelines;

public interface ISignalHandlingPipelineBuilder : ISignalPipelineBuilder
{
    ISignalHandlingPipelineBuilder Handle(ISignalHandler handler);
}
