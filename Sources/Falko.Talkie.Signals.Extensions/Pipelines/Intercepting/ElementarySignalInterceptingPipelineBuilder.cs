using System.Collections.Immutable;
using Talkie.Collections;
using Talkie.Interceptors;

namespace Talkie.Pipelines.Intercepting;

public abstract class ElementarySignalInterceptingPipelineBuilder(ImmutableStack<ISignalInterceptorFactory> interceptorFactories)
    : IReadOnlySignalInterceptingPipelineBuilder
{
    public ImmutableStack<ISignalInterceptorFactory> InterceptorFactories { get; } = interceptorFactories;

    public ISignalInterceptingPipeline Build()
    {
        var interceptors = new Sequence<ISignalInterceptor>();

        foreach (var factory in InterceptorFactories)
        {
            var interceptor = factory.Create();

            interceptors.Add(interceptor);
        }

        return SignalInterceptingPipelineFactory.Create(interceptors);
    }
}
