using Talkie.Handlers;
using Talkie.Signals;

namespace Talkie.Pipelines;

public interface ISignalHandlingPipelineBuilder<out T> : ISignalPipelineBuilder where T : Signal
{
    ISignalHandlingPipelineBuilder<T> Handle(ISignalHandler<T> handler);
}
