using Talkie.Interceptors;
using Talkie.Signals;

namespace Talkie.Pipelines;

public static partial class SignalPipelineBuilderExtensions
{
    public static ISignalInterceptingPipelineBuilder Intercept(this ISignalInterceptingPipelineBuilder builder,
        Func<Signal, CancellationToken, InterceptionResult> intercept)
    {
        return builder.Intercept(new DelegatedSignalInterceptor(intercept));
    }

    public static ISignalInterceptingPipelineBuilder Intercept(this ISignalInterceptingPipelineBuilder builder,
        Func<Signal, InterceptionResult> intercept)
    {
        return builder.Intercept((signal, _) => intercept(signal));
    }

    public static ISignalInterceptingPipelineBuilder<T> Intercept<T>(this ISignalInterceptingPipelineBuilder<T> builder,
        Func<T, CancellationToken, InterceptionResult> intercept)
        where T : Signal
    {
        return builder.Intercept(new DelegatedSignalInterceptor<T>(intercept));
    }

    public static ISignalInterceptingPipelineBuilder<T> Intercept<T>(this ISignalInterceptingPipelineBuilder<T> builder,
        Func<T, InterceptionResult> intercept)
        where T : Signal
    {
        return builder.Intercept((signal, _) => intercept(signal));
    }
}
