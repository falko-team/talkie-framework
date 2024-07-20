using Talkie.Interceptors;
using Talkie.Pipelines.Intercepting;
using Talkie.Signals;

namespace Talkie.Piepelines2.Intercepting;

public sealed class ImmutableSignalInterceptingPipelineBuilder<T>
    : PrimitiveImmutableSignalInterceptingPipelineBuilder, ISignalInterceptingPipelineBuilder<T> where T : Signal
{
    public static readonly ImmutableSignalInterceptingPipelineBuilder<T> Instance = new();

    private ImmutableSignalInterceptingPipelineBuilder() { }

    public ImmutableSignalInterceptingPipelineBuilder(IReadOnlySignalInterceptingPipelineBuilder builder)
        : base(builder) { }

    private ImmutableSignalInterceptingPipelineBuilder(ImmutableSignalInterceptingPipelineBuilder<T> builder,
        ISignalInterceptorFactory interceptorFactory) : base(builder, interceptorFactory) { }

    public ISignalInterceptingPipelineBuilder<T> Intercept(ISignalInterceptorFactory<ISignalInterceptor<T>> interceptorFactory)
    {
        return new ImmutableSignalInterceptingPipelineBuilder<T>(this, interceptorFactory);
    }

    public ISignalInterceptingPipelineBuilder Intercept(ISignalInterceptorFactory interceptorFactory)
    {
        return new ImmutableSignalInterceptingPipelineBuilder<T>(this, interceptorFactory);
    }
}
