using Falko.Talkie.Handlers;
using Falko.Talkie.Pipelines.Intercepting;
using Falko.Talkie.Signals;

namespace Falko.Talkie.Pipelines.Handling;

public sealed class SignalHandlingPipelineBuilder<T>
    : ElementarySignalHandlingPipelineBuilder, ISignalHandlingPipelineBuilder<T> where T : Signal
{
    public static readonly SignalHandlingPipelineBuilder<T> Empty = new();

    private SignalHandlingPipelineBuilder() { }

    public SignalHandlingPipelineBuilder(ISignalInterceptingPipeline? pipeline) : base(pipeline) { }

    public ISignalHandlingPipelineBuilder<T> HandleAsync(ISignalHandler<T> handler)
    {
        AddHandler(handler);

        return this;
    }
}
