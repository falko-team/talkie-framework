using System.Collections.Immutable;
using Talkie.Interceptors;
using Talkie.Sequences;

namespace Talkie.Pipelines.Intercepting;

public abstract class ElementarySignalInterceptingPipelineBuilder(ImmutableStack<ISignalInterceptorFactory> interceptorFactories)
    : IReadOnlySignalInterceptingPipelineBuilder
{
    public ImmutableStack<ISignalInterceptorFactory> InterceptorFactories { get; } = interceptorFactories;

    public ISignalInterceptingPipeline Build()
    {
        var interceptors = new Stack<ISignalInterceptor>();

        foreach (var factory in InterceptorFactories)
        {
            var interceptor = factory.Create();

            interceptors.Push(interceptor);
        }

        return SignalInterceptingPipelineFactory.Create(interceptors.ToFrozenSequence());
    }
}
