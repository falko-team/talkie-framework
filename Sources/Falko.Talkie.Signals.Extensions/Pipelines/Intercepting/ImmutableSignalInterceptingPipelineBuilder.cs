namespace Talkie.Piepelines2.Intercepting;

public sealed class ImmutableSignalInterceptingPipelineBuilder
    : PrimitiveImmutableSignalInterceptingPipelineBuilder, ISignalInterceptingPipelineBuilder
{
    public static readonly ImmutableSignalInterceptingPipelineBuilder Instance = new();

    private ImmutableSignalInterceptingPipelineBuilder() { }

    public ImmutableSignalInterceptingPipelineBuilder(IReadOnlySignalInterceptingPipelineBuilder builder)
        : base(builder) { }

    private ImmutableSignalInterceptingPipelineBuilder(ImmutableSignalInterceptingPipelineBuilder builder,
        ISignalInterceptorFactory interceptorFactory) : base(builder, interceptorFactory) { }

    public ISignalInterceptingPipelineBuilder Intercept(ISignalInterceptorFactory interceptorFactory)
    {
        return new ImmutableSignalInterceptingPipelineBuilder(this, interceptorFactory);
    }
}
