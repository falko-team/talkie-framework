using Falko.Talkie.Interceptors;
using Falko.Talkie.Signals;

namespace Falko.Talkie.Pipelines;

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
