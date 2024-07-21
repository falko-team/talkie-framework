using Talkie.Handlers;
using Talkie.Pipelines.Intercepting;

namespace Talkie.Pipelines.Handling;

public sealed class SignalHandlingPipelineBuilder : ElementarySignalHandlingPipelineBuilder, ISignalHandlingPipelineBuilder
{
    public static readonly SignalHandlingPipelineBuilder Empty = new();

    private SignalHandlingPipelineBuilder() { }

    public SignalHandlingPipelineBuilder(ISignalInterceptingPipeline pipeline) : base(pipeline) { }

    public ISignalHandlingPipelineBuilder HandleAsync(ISignalHandler handler)
    {
        AddHandler(handler);

        return this;
    }
}
