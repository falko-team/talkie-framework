using Talkie.Handlers;
using Talkie.Signals;

namespace Talkie.Pipelines;

public interface ISignalHandlingPipelineBuilder<out T> : ISignalPipelineBuilder where T : Signal
{
    ISignalHandlingPipelineBuilder<T> HandleAsync(ISignalHandler<T> handler);

    new ISignalHandlingPipelineBuilder<T> Copy();
}
