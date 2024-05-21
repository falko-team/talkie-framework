using Talkie.Collections;
using Talkie.Handlers;
using Talkie.Interceptors;

namespace Talkie.Pipelines;

public sealed class SignalHandlingPipelineBuilder : ISignalHandlingPipelineBuilder
{
    private readonly FrozenSequence<ISignalInterceptor> _interceptors;

    private readonly Sequence<ISignalHandler> _handlers;

    public SignalHandlingPipelineBuilder(IEnumerable<ISignalInterceptor> interceptors, IEnumerable<ISignalHandler> handlers)
    {
        _interceptors = interceptors.ToFrozenSequence();
        _handlers = handlers.ToSequence();
    }

    public SignalHandlingPipelineBuilder(IEnumerable<ISignalInterceptor> interceptors)
    {
        _interceptors = interceptors.ToFrozenSequence();
        _handlers = new Sequence<ISignalHandler>();
    }

    public ISignalHandlingPipelineBuilder Handle(ISignalHandler handler)
    {
        _handlers.Add(handler);

        return this;
    }

    public ISignalPipeline Build()
    {
        return new SignalPipeline(_interceptors, _handlers);
    }

    public FrozenSequence<ISignalInterceptor> ToInterceptors()
    {
        return _interceptors;
    }

    public FrozenSequence<ISignalHandler> ToHandlers()
    {
        return _handlers.ToFrozenSequence();
    }
}
