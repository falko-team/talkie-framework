using Talkie.Handlers;

namespace Talkie.Pipelines;

public interface ISignalHandlingPipelineBuilder : ISignalPipelineBuilder
{
    ISignalHandlingPipelineBuilder HandleAsync(ISignalHandler handler);

    new ISignalHandlingPipelineBuilder Copy();
}
