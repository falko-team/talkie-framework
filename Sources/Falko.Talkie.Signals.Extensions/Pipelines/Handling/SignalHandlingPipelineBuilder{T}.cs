using Talkie.Handlers;
using Talkie.Pipelines.Intercepting;
using Talkie.Signals;

namespace Talkie.Pipelines.Handling;

public sealed class SignalHandlingPipelineBuilder<T>
    : ElementarySignalHandlingPipelineBuilder, ISignalHandlingPipelineBuilder<T> where T : Signal
{
    public static readonly SignalHandlingPipelineBuilder<T> Empty = new();

    private SignalHandlingPipelineBuilder() { }

    public SignalHandlingPipelineBuilder(ISignalInterceptingPipeline pipeline) : base(pipeline) { }

    public ISignalHandlingPipelineBuilder<T> HandleAsync(ISignalHandler<T> handler)
    {
        AddHandler(handler);

        return this;
    }
}
