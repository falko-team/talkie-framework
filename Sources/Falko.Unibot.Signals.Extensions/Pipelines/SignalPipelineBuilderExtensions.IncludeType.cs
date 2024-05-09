using Falko.Unibot.Interceptors;
using Falko.Unibot.Signals;

namespace Falko.Unibot.Pipelines;

public static partial class SignalPipelineBuilderExtensions
{
    public static ISignalInterceptingPipelineBuilder IncludeType<T>(this ISignalInterceptingPipelineBuilder builder)
        where T : Signal
    {
        return builder.Intercept(IncludeTypeSignalInterceptor<T>.Instance);
    }

    public static ISignalInterceptingPipelineBuilder<TOriginal> IncludeType<TOriginal, TTyped>(this ISignalInterceptingPipelineBuilder<TOriginal> builder)
        where TOriginal : Signal
        where TTyped : TOriginal
    {
        return builder.Intercept(IncludeTypeSignalInterceptor<TOriginal, TTyped>.Instance);
    }
}
