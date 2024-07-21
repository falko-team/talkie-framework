namespace Talkie.Pipelines.Handling;

public interface ISignalHandlingPipelineBuilder : IReadOnlySignalHandlingPipelineBuilder
{
    ISignalHandlingPipelineBuilder HandleAsync(ISignalHandlerFactory handlerFactory);
}
