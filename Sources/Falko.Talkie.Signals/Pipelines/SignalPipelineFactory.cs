using Talkie.Collections;
using Talkie.Handlers;
using Talkie.Interceptors;

namespace Talkie.Pipelines;

public static class SignalPipelineFactory
{
    public static ISignalPipeline Create(IReadOnlySequence<ISignalInterceptor> interceptors,
        IReadOnlySequence<ISignalHandler> handlers)
    {
        return handlers.Count switch
        {
            0 => EmptySignalPipeline.Instance,
            1 => new SingleHandlerSignalPipeline(interceptors, handlers.First()),
            _ => new ManyHandlersSignalPipeline(interceptors, handlers)
        };
    }
}
