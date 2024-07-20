using Talkie.Piepelines2.Intercepting;
using Talkie.Pipelines.Handling;
using Talkie.Pipelines.Intercepting;
using Talkie.Validations;

namespace Talkie.Piepelines2.Handling;

public abstract class PrimitiveImmutableSignalHandlingPipelineBuilder : IReadOnlySignalHandlingPipelineBuilder
{
    private readonly IReadOnlySignalHandlingPipelineBuilder? _parent;

    private readonly HandlerFactoryNode? _node;

    protected PrimitiveImmutableSignalHandlingPipelineBuilder(ISignalInterceptingPipeline intercepting)
    {
        intercepting.ThrowIf().Null();

        Intercepting = intercepting;
    }

    protected PrimitiveImmutableSignalHandlingPipelineBuilder(IReadOnlySignalHandlingPipelineBuilder parent,
        ISignalHandlerFactory handlerFactory) : this(parent.Intercepting)
    {
        parent.ThrowIf().Null();
        handlerFactory.ThrowIf().Null();

        if (parent is PrimitiveImmutableSignalHandlingPipelineBuilder baseParent)
        {
            _node = new HandlerFactoryNode(baseParent, handlerFactory);
        }
        else
        {
            _parent = parent;
        }
    }

    protected PrimitiveImmutableSignalHandlingPipelineBuilder(PrimitiveImmutableSignalHandlingPipelineBuilder parent,
        ISignalHandlerFactory handlerFactory) : this(parent.Intercepting)
    {
        handlerFactory.ThrowIf().Null();

        _node = new HandlerFactoryNode(parent, handlerFactory);
    }

    public ISignalInterceptingPipeline Intercepting { get; }

    public IEnumerable<ISignalHandlerFactory> HandlerFactories
    {
        get
        {
            if (_parent is not null)
            {
                foreach (var factory in _parent.HandlerFactories)
                {
                    yield return factory;
                }
            }

            var factories = new Queue<ISignalHandlerFactory>();

            var current = _node;

            while (current is not null)
            {
                factories.Enqueue(current.HandlerFactory);

                current = current.Previous._node;
            }

            while (factories.TryDequeue(out var factory))
            {
                yield return factory;
            }
        }
    }

    public ISignalHandlingPipeline Build()
    {
        return SignalHandlingPipelineFactory.Create(HandlerFactories
            .Select(handlerFactory => handlerFactory.Create())
            .ToArray(), Intercepting);
    }

    private sealed class HandlerFactoryNode(PrimitiveImmutableSignalHandlingPipelineBuilder previous,
        ISignalHandlerFactory handlerFactory)
    {
        public readonly PrimitiveImmutableSignalHandlingPipelineBuilder Previous = previous;

        public readonly ISignalHandlerFactory HandlerFactory = handlerFactory;
    }
}
