using Talkie.Piepelines2.Intercepting;
using Talkie.Pipelines.Handling;
using Talkie.Pipelines.Intercepting;

namespace Talkie.Piepelines2.Handling;

public interface IReadOnlySignalHandlingPipelineBuilder
{
    ISignalInterceptingPipeline Intercepting { get; }

    IEnumerable<ISignalHandlerFactory> HandlerFactories { get; }

    ISignalHandlingPipeline Build();
}
