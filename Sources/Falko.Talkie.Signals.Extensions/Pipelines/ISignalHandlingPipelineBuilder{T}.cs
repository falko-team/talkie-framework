using Falko.Talkie.Handlers;
using Falko.Talkie.Signals;

namespace Falko.Talkie.Pipelines;

public interface ISignalHandlingPipelineBuilder<out T> : ISignalPipelineBuilder where T : Signal
{
    ISignalHandlingPipelineBuilder<T> Handle(ISignalHandler<T> handler);
}
