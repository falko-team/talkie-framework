using Falko.Talkie.Handlers;
using Falko.Talkie.Pipelines.Intercepting;

namespace Falko.Talkie.Pipelines.Handling;

public interface IReadOnlySignalHandlingPipelineBuilder
{
    ISignalInterceptingPipeline? Intercepting { get; }

    IEnumerable<ISignalHandler> Handlers { get; }

    ISignalHandlingPipeline Build();
}
