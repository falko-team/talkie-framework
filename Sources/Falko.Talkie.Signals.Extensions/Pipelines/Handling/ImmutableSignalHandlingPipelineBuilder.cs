using Talkie.Pipelines.Intercepting;

namespace Talkie.Pipelines.Handling;

public sealed class ImmutableSignalHandlingPipelineBuilder
    : PrimitiveImmutableSignalHandlingPipelineBuilder, ISignalHandlingPipelineBuilder
{
    public ImmutableSignalHandlingPipelineBuilder(ISignalInterceptingPipeline pipeline)
        : base(pipeline) { }

    private ImmutableSignalHandlingPipelineBuilder(ImmutableSignalHandlingPipelineBuilder builder,
        ISignalHandlerFactory handlerFactory) : base(builder, handlerFactory) { }

    public ISignalHandlingPipelineBuilder HandleAsync(ISignalHandlerFactory handlerFactory)
    {
        return new ImmutableSignalHandlingPipelineBuilder(this, handlerFactory);
    }
}
