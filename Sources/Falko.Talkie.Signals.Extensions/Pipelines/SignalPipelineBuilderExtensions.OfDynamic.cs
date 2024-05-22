using Talkie.Signals;

namespace Talkie.Pipelines;

public static partial class SignalPipelineBuilderExtensions
{
    public static ISignalInterceptingPipelineBuilder OfDynamic<T>(this ISignalInterceptingPipelineBuilder<T> builder)
        where T : Signal
    {
        return new SignalInterceptingPipelineBuilder(builder.CopyInterceptors());
    }

    public static ISignalHandlingPipelineBuilder OfDynamic<T>(this ISignalHandlingPipelineBuilder<T> builder)
        where T : Signal
    {
        return new SignalHandlingPipelineBuilder(builder.CopyInterceptors(), builder.CopyHandlers());
    }
}
