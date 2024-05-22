using Talkie.Signals;

namespace Talkie.Pipelines;

public static partial class SignalPipelineBuilderExtensions
{
    public static ISignalInterceptingPipelineBuilder<TTo> OfType<TFrom, TTo>(this ISignalInterceptingPipelineBuilder<TFrom> builder)
        where TFrom : Signal
        where TTo : TFrom
    {
        return new SignalInterceptingPipelineBuilder<TTo>(builder.CopyInterceptors());
    }

    public static ISignalHandlingPipelineBuilder<TTo> OfType<TFrom, TTo>(this ISignalHandlingPipelineBuilder<TFrom> builder)
        where TFrom : Signal
        where TTo : TFrom
    {
        return new SignalHandlingPipelineBuilder<TTo>(builder.CopyInterceptors(), builder.CopyHandlers());
    }

    public static ISignalInterceptingPipelineBuilder<T> OfType<T>(this ISignalInterceptingPipelineBuilder builder)
        where T : Signal
    {
        return new SignalInterceptingPipelineBuilder<T>(builder.CopyInterceptors());
    }

    public static ISignalHandlingPipelineBuilder<T> OfType<T>(this ISignalHandlingPipelineBuilder builder)
        where T : Signal
    {
        return new SignalHandlingPipelineBuilder<T>(builder.CopyInterceptors(), builder.CopyHandlers());
    }
}
