using System.Collections.Immutable;
using Falko.Talkie.Interceptors;
using Falko.Talkie.Sequences;

namespace Falko.Talkie.Pipelines.Intercepting;

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
