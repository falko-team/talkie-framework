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

        var interceptorFactories = InterceptorFactories;

        while (interceptorFactories.IsEmpty is false)
        {
            var interceptorFactory = interceptorFactories.Peek();

            interceptors.Add(interceptorFactory.Create());

            interceptorFactories = interceptorFactories.Pop();
        }

        return SignalInterceptingPipelineFactory.Create(interceptors);
    }
}
