using Talkie.Pipelines.Intercepting;
using Talkie.Validations;

namespace Talkie.Piepelines2.Intercepting;

public abstract class PrimitiveImmutableSignalInterceptingPipelineBuilder : IReadOnlySignalInterceptingPipelineBuilder
{
    private readonly IReadOnlySignalInterceptingPipelineBuilder? _parent;

    private readonly InterceptorFactoryNode? _node;

    protected PrimitiveImmutableSignalInterceptingPipelineBuilder() { }

    protected PrimitiveImmutableSignalInterceptingPipelineBuilder(IReadOnlySignalInterceptingPipelineBuilder parent)
    {
        parent.ThrowIf().Null();

        _parent = parent;
    }

    protected PrimitiveImmutableSignalInterceptingPipelineBuilder(PrimitiveImmutableSignalInterceptingPipelineBuilder parent,
        ISignalInterceptorFactory interceptorFactory)
    {
        interceptorFactory.ThrowIf().Null();

        _node = new InterceptorFactoryNode(parent, interceptorFactory);
    }

    public IEnumerable<ISignalInterceptorFactory> InterceptorFactories
    {
        get
        {
            if (_parent is not null)
            {
                foreach (var factory in _parent.InterceptorFactories)
                {
                    yield return factory;
                }
            }

            var factories = new Queue<ISignalInterceptorFactory>();

            var current = _node;

            while (current is not null)
            {
                factories.Enqueue(current.InterceptorFactory);

                current = current.Previous._node;
            }

            while (factories.TryDequeue(out var handler))
            {
                yield return handler;
            }
        }
    }

    public ISignalInterceptingPipeline Build()
    {
        return SignalInterceptingPipelineFactory.Create(InterceptorFactories
            .Select(interceptorFactory => interceptorFactory.Create())
            .ToArray());
    }

    private sealed class InterceptorFactoryNode(PrimitiveImmutableSignalInterceptingPipelineBuilder previous,
        ISignalInterceptorFactory interceptorFactory)
    {
        public readonly PrimitiveImmutableSignalInterceptingPipelineBuilder Previous = previous;

        public readonly ISignalInterceptorFactory InterceptorFactory = interceptorFactory;
    }
}
