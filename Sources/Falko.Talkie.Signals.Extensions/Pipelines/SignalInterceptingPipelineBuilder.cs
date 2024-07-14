using Talkie.Collections;
using Talkie.Handlers;
using Talkie.Interceptors;

namespace Talkie.Pipelines;

public sealed class SignalInterceptingPipelineBuilder : ISignalInterceptingPipelineBuilder
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

    public ISignalInterceptingPipelineBuilder Intercept(ISignalInterceptor interceptor)
    {
        _interceptors.Add(interceptor);

        return this;
    }

    public ISignalHandlingPipelineBuilder HandleAsync(ISignalHandler handler)
    {
        return new SignalHandlingPipelineBuilder(_interceptors).HandleAsync(handler);
    }

    public ISignalPipeline Build() => EmptySignalPipeline.Instance;

    public FrozenSequence<ISignalInterceptor> CopyInterceptors() => _interceptors.ToFrozenSequence();

    public FrozenSequence<ISignalHandler> CopyHandlers() => FrozenSequence<ISignalHandler>.Empty;

    public ISignalInterceptingPipelineBuilder Copy() => new SignalInterceptingPipelineBuilder(_interceptors);

    ISignalPipelineBuilder ISignalPipelineBuilder.Copy() => Copy();
}
