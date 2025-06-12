using System.Collections.Immutable;
using Falko.Talkie.Signals;

namespace Falko.Talkie.Pipelines.Intercepting;

public sealed class SignalInterceptingPipelineBuilder<T>(ImmutableStack<ISignalInterceptorFactory> interceptorFactories)
    : ElementarySignalInterceptingPipelineBuilder(interceptorFactories), ISignalInterceptingPipelineBuilder<T>
        where T : Signal
{
    public static readonly SignalInterceptingPipelineBuilder<T> Empty = new();

    private SignalInterceptingPipelineBuilder() : this(ImmutableStack<ISignalInterceptorFactory>.Empty) { }

    public ISignalInterceptingPipelineBuilder<T> Intercept(ISignalInterceptorFactory<T> interceptor)
    {
        return new SignalInterceptingPipelineBuilder<T>(InterceptorFactories.Push(interceptor));
    }
}
