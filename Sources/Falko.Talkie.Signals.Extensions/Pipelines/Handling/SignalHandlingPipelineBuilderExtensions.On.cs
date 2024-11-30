using Talkie.Pipelines.Intercepting;
using Talkie.Signals;

namespace Talkie.Pipelines.Handling;

public static partial class SignalHandlingPipelineBuilderExtensions
{
    public static ISignalHandlingPipelineBuilder HandleOn
    (
        this ISignalInterceptingPipelineBuilder builder,
        ISignalHandlingPipelineProcessorFactory processorFactory
    )
    {
        var pipeline = builder.Build();

        return pipeline is not EmptySignalInterceptingPipeline
            ? new SignalHandlingPipelineBuilder(pipeline)
            : SignalHandlingPipelineBuilder.Empty;
    }

    public static ISignalHandlingPipelineBuilder<T> HandleOn<T>
    (
        this ISignalInterceptingPipelineBuilder<T> builder,
        ISignalHandlingPipelineProcessorFactory processorFactory
    ) where T : Signal
    {
        var pipeline = builder.Build();

        return pipeline is not EmptySignalInterceptingPipeline
            ? new SignalHandlingPipelineBuilder<T>(pipeline)
            : SignalHandlingPipelineBuilder<T>.Empty;
    }

    public static ISignalInterceptingPipelineBuilder<T> InterceptOn<T>
    (
        this ISignalInterceptingPipelineBuilder<T> builder,
        ISignalInterceptingPipelineProcessorFactory processorFactory
    ) where T : Signal
    {
        var pipeline = builder.Build();

        return builder;
    }
}
