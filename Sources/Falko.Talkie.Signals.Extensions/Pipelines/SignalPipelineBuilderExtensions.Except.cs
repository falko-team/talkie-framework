using Talkie.Signals;

namespace Talkie.Pipelines;

public static partial class SignalPipelineBuilderExtensions
{
    public static ISignalInterceptingPipelineBuilder Except<T>(this ISignalInterceptingPipelineBuilder builder)
        where T : Signal
    {
        return builder.Where(signal => signal is not T);
    }

    public static ISignalInterceptingPipelineBuilder Except<T1, T2>(this ISignalInterceptingPipelineBuilder builder)
    {
        return builder.Where(signal => signal is not T1 is not T2);
    }

    public static ISignalInterceptingPipelineBuilder Except<T1, T2, T3>(this ISignalInterceptingPipelineBuilder builder)
    {
        return builder.Where(signal => signal is not T1 is not T2 is not T3);
    }

    public static ISignalInterceptingPipelineBuilder Except<T1, T2, T3, T4>(this ISignalInterceptingPipelineBuilder builder)
    {
        return builder.Where(signal => signal is not T1 is not T2 is not T3 is not T4);
    }

    public static ISignalInterceptingPipelineBuilder Except<T1, T2, T3, T4, T5>(this ISignalInterceptingPipelineBuilder builder)
    {
        return builder.Where(signal => signal is not T1 is not T2 is not T3 is not T4 is not T5);
    }

    public static ISignalInterceptingPipelineBuilder Except<T1, T2, T3, T4, T5, T6>(this ISignalInterceptingPipelineBuilder builder)
    {
        return builder.Where(signal => signal is not T1 is not T2 is not T3 is not T4 is not T5 is not T6);
    }

    public static ISignalInterceptingPipelineBuilder<TOriginal> Except<TOriginal, T>(this ISignalInterceptingPipelineBuilder<TOriginal> builder)
        where TOriginal : Signal
        where T : TOriginal
    {
        return builder.Where(signal => signal is not T);
    }

    public static ISignalInterceptingPipelineBuilder<TOriginal> Except<TOriginal, T1, T2>(this ISignalInterceptingPipelineBuilder<TOriginal> builder)
        where TOriginal : Signal
        where T1 : TOriginal
        where T2 : TOriginal
    {
        return builder.Where(signal => signal is not T1 is not T2);
    }

    public static ISignalInterceptingPipelineBuilder<TOriginal> Except<TOriginal, T1, T2, T3>(this ISignalInterceptingPipelineBuilder<TOriginal> builder)
        where TOriginal : Signal
        where T1 : TOriginal
        where T2 : TOriginal
        where T3 : TOriginal
    {
        return builder.Where(signal => signal is not T1 is not T2 is not T3);
    }

    public static ISignalInterceptingPipelineBuilder<TOriginal> Except<TOriginal, T1, T2, T3, T4>(this ISignalInterceptingPipelineBuilder<TOriginal> builder)
        where TOriginal : Signal
        where T1 : TOriginal
        where T2 : TOriginal
        where T3 : TOriginal
        where T4 : TOriginal
    {
        return builder.Where(signal => signal is not T1 is not T2 is not T3 is not T4);
    }

    public static ISignalInterceptingPipelineBuilder<TOriginal> Except<TOriginal, T1, T2, T3, T4, T5>(this ISignalInterceptingPipelineBuilder<TOriginal> builder)
        where TOriginal : Signal
        where T1 : TOriginal
        where T2 : TOriginal
        where T3 : TOriginal
        where T4 : TOriginal
        where T5 : TOriginal
    {
        return builder.Where(signal => signal is not T1 is not T2 is not T3 is not T4 is not T5);
    }

    public static ISignalInterceptingPipelineBuilder<TOriginal> Except<TOriginal, T1, T2, T3, T4, T5, T6>(this ISignalInterceptingPipelineBuilder<TOriginal> builder)
        where TOriginal : Signal
        where T1 : TOriginal
        where T2 : TOriginal
        where T3 : TOriginal
        where T4 : TOriginal
        where T5 : TOriginal
        where T6 : TOriginal
    {
        return builder.Where(signal => signal is not T1 is not T2 is not T3 is not T4 is not T5 is not T6);
    }
}
