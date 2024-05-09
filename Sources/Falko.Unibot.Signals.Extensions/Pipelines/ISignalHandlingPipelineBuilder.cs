using Falko.Unibot.Handlers;

namespace Falko.Unibot.Pipelines;

public interface ISignalHandlingPipelineBuilder : ISignalPipelineBuilder
{
    ISignalHandlingPipelineBuilder Handle(ISignalHandler handler);
}
