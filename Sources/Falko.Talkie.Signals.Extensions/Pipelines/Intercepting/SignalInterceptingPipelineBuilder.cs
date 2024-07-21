using System.Collections.Immutable;

namespace Talkie.Pipelines.Intercepting;

public sealed class SignalInterceptingPipelineBuilder(ImmutableStack<ISignalInterceptorFactory> interceptorFactories)
    : ElementarySignalInterceptingPipelineBuilder(interceptorFactories), ISignalInterceptingPipelineBuilder
{
    public static readonly SignalInterceptingPipelineBuilder Empty = new();

    private SignalInterceptingPipelineBuilder() : this(ImmutableStack<ISignalInterceptorFactory>.Empty) { }

    public ISignalInterceptingPipelineBuilder Intercept(ISignalInterceptorFactory interceptorFactory)
    {
        return new SignalInterceptingPipelineBuilder(InterceptorFactories.Push(interceptorFactory));
    }
}
