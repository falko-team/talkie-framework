using Talkie.Pipelines.Intercepting;

namespace Talkie.Pipelines.Handling;

public interface IReadOnlySignalHandlingPipelineBuilder
{
    ISignalInterceptingPipeline Intercepting { get; }

    IEnumerable<ISignalHandlerFactory> HandlerFactories { get; }

    ISignalHandlingPipeline Build();
}
