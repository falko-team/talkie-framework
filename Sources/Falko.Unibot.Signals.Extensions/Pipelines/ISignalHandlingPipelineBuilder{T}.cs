using Falko.Unibot.Handlers;
using Falko.Unibot.Signals;

namespace Falko.Unibot.Pipelines;

public interface ISignalHandlingPipelineBuilder<out T> : ISignalPipelineBuilder where T : Signal
{
    ISignalHandlingPipelineBuilder<T> Handle(ISignalHandler<T> handler);
}
