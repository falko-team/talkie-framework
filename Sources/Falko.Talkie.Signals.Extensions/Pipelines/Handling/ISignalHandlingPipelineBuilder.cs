namespace Talkie.Piepelines2.Handling;

public interface ISignalHandlingPipelineBuilder : IReadOnlySignalHandlingPipelineBuilder
{
    ISignalHandlingPipelineBuilder HandleAsync(ISignalHandlerFactory handlerFactory);
}
