using Talkie.Collections;
using Talkie.Handlers;
using Talkie.Interceptors;

namespace Talkie.Pipelines;

public sealed class SignalHandlingPipelineBuilder : ISignalHandlingPipelineBuilder
{
    private readonly FrozenSequence<ISignalInterceptor> _interceptors;

    private readonly Sequence<ISignalHandler> _handlers;

    internal SignalHandlingPipelineBuilder(IEnumerable<ISignalInterceptor> interceptors, IEnumerable<ISignalHandler> handlers)
    {
        _interceptors = interceptors.ToFrozenSequence();
        _handlers = handlers.ToSequence();
    }

    internal SignalHandlingPipelineBuilder(IEnumerable<ISignalInterceptor> interceptors)
    {
        _interceptors = interceptors.ToFrozenSequence();
        _handlers = new Sequence<ISignalHandler>();
    }

    public SignalHandlingPipelineBuilder()
    {
        _interceptors = FrozenSequence<ISignalInterceptor>.Empty;
        _handlers = new Sequence<ISignalHandler>();
    }

    public ISignalHandlingPipelineBuilder HandleAsync(ISignalHandler handler)
    {
        _handlers.Add(handler);

        return this;
    }

    public ISignalPipeline Build() => new SignalPipeline(_interceptors, _handlers);

    public FrozenSequence<ISignalInterceptor> CopyInterceptors() => _interceptors;

    public FrozenSequence<ISignalHandler> CopyHandlers() => _handlers.ToFrozenSequence();
}
