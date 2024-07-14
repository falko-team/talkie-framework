using Talkie.Collections;
using Talkie.Handlers;
using Talkie.Interceptors;
using Talkie.Signals;

namespace Talkie.Pipelines;

public sealed class SignalInterceptingPipelineBuilder<T> : ISignalInterceptingPipelineBuilder<T> where T : Signal
{
    private readonly Sequence<ISignalInterceptor> _interceptors;

    internal SignalInterceptingPipelineBuilder(IEnumerable<ISignalInterceptor> interceptors)
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

    public ISignalHandlingPipelineBuilder<T> HandleAsync(ISignalHandler<T> handler)
    {
        return new SignalHandlingPipelineBuilder<T>(_interceptors).HandleAsync(handler);
    }

    public ISignalPipeline Build() => EmptySignalPipeline.Instance;

    public FrozenSequence<ISignalInterceptor> CopyInterceptors() => _interceptors.ToFrozenSequence();

    public FrozenSequence<ISignalHandler> CopyHandlers() => FrozenSequence<ISignalHandler>.Empty;

    public ISignalInterceptingPipelineBuilder<T> Copy() => new SignalInterceptingPipelineBuilder<T>(_interceptors);

    ISignalPipelineBuilder ISignalPipelineBuilder.Copy() => Copy();
}
