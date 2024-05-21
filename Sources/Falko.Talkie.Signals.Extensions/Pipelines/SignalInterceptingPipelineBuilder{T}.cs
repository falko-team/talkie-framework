using Talkie.Collections;
using Talkie.Handlers;
using Talkie.Interceptors;
using Talkie.Signals;

namespace Talkie.Pipelines;

public sealed class SignalInterceptingPipelineBuilder<T> : ISignalInterceptingPipelineBuilder<T> where T : Signal
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

    public ISignalInterceptingPipelineBuilder<T> Intercept(ISignalInterceptor<T> interceptor)
    {
        _interceptors.Add(interceptor);

        return this;
    }

    public ISignalHandlingPipelineBuilder<T> Handle(ISignalHandler<T> handler)
    {
        return new SignalHandlingPipelineBuilder<T>(_interceptors).Handle(handler);
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
