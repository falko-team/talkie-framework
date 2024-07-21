using Talkie.Handlers;
using Talkie.Pipelines.Intercepting;

namespace Talkie.Pipelines.Handling;

public interface IReadOnlySignalHandlingPipelineBuilder
{
    ISignalInterceptingPipeline? Intercepting { get; }

    IEnumerable<ISignalHandler> Handlers { get; }

    ISignalHandlingPipeline Build();
}
