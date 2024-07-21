using Talkie.Collections;
using Talkie.Handlers;
using Talkie.Pipelines.Intercepting;
using Talkie.Validations;

namespace Talkie.Pipelines.Handling;

public abstract class ElementarySignalHandlingPipelineBuilder : IReadOnlySignalHandlingPipelineBuilder
{
    private readonly Sequence<ISignalHandler> _handlers = new();

    protected ElementarySignalHandlingPipelineBuilder() { }

    protected ElementarySignalHandlingPipelineBuilder(ISignalInterceptingPipeline interceptingPipeline)
    {
        interceptingPipeline.ThrowIf().Null();

        Intercepting = interceptingPipeline;
    }

    public ISignalInterceptingPipeline? Intercepting { get; }

    public IEnumerable<ISignalHandler> Handlers => _handlers;

    public ISignalHandlingPipeline Build() => SignalHandlingPipelineFactory.Create(Handlers, Intercepting);

    protected void AddHandler(ISignalHandler handler) => _handlers.Add(handler);
}
