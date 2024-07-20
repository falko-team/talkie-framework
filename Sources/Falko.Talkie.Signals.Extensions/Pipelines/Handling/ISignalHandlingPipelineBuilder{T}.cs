using Talkie.Handlers;
using Talkie.Piepelines2.Intercepting;
using Talkie.Signals;

namespace Talkie.Piepelines2.Handling;

public interface ISignalHandlingPipelineBuilder<out T> : IReadOnlySignalHandlingPipelineBuilder where T : Signal
{
    ISignalHandlingPipelineBuilder<T> HandleAsync(ISignalHandlerFactory<ISignalHandler<T>> handlerFactory);
}
