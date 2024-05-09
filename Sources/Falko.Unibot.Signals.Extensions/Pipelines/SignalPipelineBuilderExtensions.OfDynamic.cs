using Falko.Unibot.Signals;

namespace Falko.Unibot.Pipelines;

public static partial class SignalPipelineBuilderExtensions
{
    public static ISignalInterceptingPipelineBuilder OfDynamic<T>(this ISignalInterceptingPipelineBuilder<T> builder)
        where T : Signal
    {
        return new SignalInterceptingPipelineBuilder(builder.ToInterceptors());
    }

    public static ISignalHandlingPipelineBuilder OfDynamic<T>(this ISignalHandlingPipelineBuilder<T> builder)
        where T : Signal
    {
        return new SignalHandlingPipelineBuilder(builder.ToInterceptors(), builder.ToHandlers());
    }
}
