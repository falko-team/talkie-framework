using Falko.Talkie.Handlers;
using Falko.Talkie.Signals;

namespace Falko.Talkie.Pipelines.Handling;

public interface ISignalHandlingPipelineBuilder<out T> : IReadOnlySignalHandlingPipelineBuilder where T : Signal
{
    ISignalHandlingPipelineBuilder<T> HandleAsync(ISignalHandler<T> handler);
}
