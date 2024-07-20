using Talkie.Handlers;
using Talkie.Piepelines2.Intercepting;
using Talkie.Pipelines.Intercepting;
using Talkie.Signals;

namespace Talkie.Piepelines2.Handling;

public sealed class ImmutableSignalHandlingPipelineBuilder<T>
    : PrimitiveImmutableSignalHandlingPipelineBuilder, ISignalHandlingPipelineBuilder<T> where T : Signal
{
    public ImmutableSignalHandlingPipelineBuilder(ISignalInterceptingPipeline pipeline)
        : base(pipeline) { }

    private ImmutableSignalHandlingPipelineBuilder(ImmutableSignalHandlingPipelineBuilder<T> builder,
        ISignalHandlerFactory<ISignalHandler<T>> handlerFactory) : base(builder, handlerFactory) { }

    public ISignalHandlingPipelineBuilder<T> HandleAsync(ISignalHandlerFactory<ISignalHandler<T>> handlerFactory)
    {
        return new ImmutableSignalHandlingPipelineBuilder<T>(this, handlerFactory);
    }
}
