using Talkie.Collections;
using Talkie.Handlers;
using Talkie.Interceptors;

namespace Talkie.Pipelines;

public sealed class SignalInterceptingPipelineBuilder : ISignalInterceptingPipelineBuilder
{
    private readonly Sequence<ISignalInterceptor> _interceptors;

    public SignalInterceptingPipelineBuilder(IEnumerable<ISignalInterceptor> interceptors)
    {
        _interceptors = interceptors.ToSequence();
    }

    public SignalInterceptingPipelineBuilder()
    {
        _interceptors = new Sequence<ISignalInterceptor>();
    }

    public ISignalInterceptingPipelineBuilder Intercept(ISignalInterceptor interceptor)
    {
        _interceptors.Add(interceptor);

        return this;
    }

    public ISignalHandlingPipelineBuilder Handle(ISignalHandler handler)
    {
        return new SignalHandlingPipelineBuilder(_interceptors).Handle(handler);
    }

    public ISignalPipeline Build()
    {
        return EmptySignalPipeline.Instance;
    }

    public FrozenSequence<ISignalInterceptor> ToInterceptors()
    {
        return _interceptors.ToFrozenSequence();
    }

    public FrozenSequence<ISignalHandler> ToHandlers()
    {
        return FrozenSequence<ISignalHandler>.Empty;
    }
}
