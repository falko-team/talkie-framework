using Falko.Talkie.Signals;

namespace Falko.Talkie.Pipelines.Intercepting;

public static partial class SignalInterceptingPipelineBuilderExtensions
{
    public static ISignalInterceptingPipelineBuilder Only<T>(this ISignalInterceptingPipelineBuilder builder)
        where T : Signal
    {
        return builder.Where(signal => signal is T);
    }

    public static ISignalInterceptingPipelineBuilder Only<T1, T2>(this ISignalInterceptingPipelineBuilder builder)
    {
        return builder.Where(signal => signal is T1 or T2);
    }

    public static ISignalInterceptingPipelineBuilder Only<T1, T2, T3>(this ISignalInterceptingPipelineBuilder builder)
    {
        return builder.Where(signal => signal is T1 or T2 or T3);
    }

    public static ISignalInterceptingPipelineBuilder Only<T1, T2, T3, T4>(this ISignalInterceptingPipelineBuilder builder)
    {
        return builder.Where(signal => signal is T1 or T2 or T3 or T4);
    }

    public static ISignalInterceptingPipelineBuilder Only<T1, T2, T3, T4, T5>(this ISignalInterceptingPipelineBuilder builder)
    {
        return builder.Where(signal => signal is T1 or T2 or T3 or T4 or T5);
    }

    public static ISignalInterceptingPipelineBuilder Only<T1, T2, T3, T4, T5, T6>(this ISignalInterceptingPipelineBuilder builder)
    {
        return builder.Where(signal => signal is T1 or T2 or T3 or T4 or T5 or T6);
    }

    public static ISignalInterceptingPipelineBuilder<TOriginal> Only<TOriginal, T>(this ISignalInterceptingPipelineBuilder<TOriginal> builder)
        where TOriginal : Signal
        where T : TOriginal
    {
        return builder.Where(signal => signal is T);
    }

    public static ISignalInterceptingPipelineBuilder<TOriginal> Only<TOriginal, T1, T2>(this ISignalInterceptingPipelineBuilder<TOriginal> builder)
        where TOriginal : Signal
        where T1 : TOriginal
        where T2 : TOriginal
    {
        return builder.Where(signal => signal is T1 or T2);
    }

    public static ISignalInterceptingPipelineBuilder<TOriginal> Only<TOriginal, T1, T2, T3>(this ISignalInterceptingPipelineBuilder<TOriginal> builder)
        where TOriginal : Signal
        where T1 : TOriginal
        where T2 : TOriginal
        where T3 : TOriginal
    {
        return builder.Where(signal => signal is T1 or T2 or T3);
    }

    public static ISignalInterceptingPipelineBuilder<TOriginal> Only<TOriginal, T1, T2, T3, T4>(this ISignalInterceptingPipelineBuilder<TOriginal> builder)
        where TOriginal : Signal
        where T1 : TOriginal
        where T2 : TOriginal
        where T3 : TOriginal
        where T4 : TOriginal
    {
        return builder.Where(signal => signal is T1 or T2 or T3 or T4);
    }

    public static ISignalInterceptingPipelineBuilder<TOriginal> Only<TOriginal, T1, T2, T3, T4, T5>(this ISignalInterceptingPipelineBuilder<TOriginal> builder)
        where TOriginal : Signal
        where T1 : TOriginal
        where T2 : TOriginal
        where T3 : TOriginal
        where T4 : TOriginal
        where T5 : TOriginal
    {
        return builder.Where(signal => signal is T1 or T2 or T3 or T4 or T5);
    }

    public static ISignalInterceptingPipelineBuilder<TOriginal> Only<TOriginal, T1, T2, T3, T4, T5, T6>(this ISignalInterceptingPipelineBuilder<TOriginal> builder)
        where TOriginal : Signal
        where T1 : TOriginal
        where T2 : TOriginal
        where T3 : TOriginal
        where T4 : TOriginal
        where T5 : TOriginal
        where T6 : TOriginal
    {
        return builder.Where(signal => signal is T1 or T2 or T3 or T4 or T5 or T6);
    }
}
