using Talkie.Handlers;
using Talkie.Signals;

namespace Talkie.Pipelines.Handling;

public interface ISignalHandlingPipelineBuilder<out T> : IReadOnlySignalHandlingPipelineBuilder where T : Signal
{
    ISignalHandlingPipelineBuilder<T> HandleAsync(ISignalHandler<T> handler);
}
