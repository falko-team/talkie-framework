using Talkie.Interceptors;
using Talkie.Signals;

namespace Talkie.Pipelines.Intercepting;

public static partial class SignalInterceptingPipelineBuilderExtensions
{
    public static ISignalInterceptingPipelineBuilder Intercept(this ISignalInterceptingPipelineBuilder builder,
        Func<Signal, CancellationToken, InterceptionResult> intercept)
    {
        return builder.InterceptSingleton(() => new DelegatedSignalInterceptor(intercept));
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
        return builder.InterceptSingleton(() => new DelegatedSignalInterceptor<T>(intercept));
    }

    public static ISignalInterceptingPipelineBuilder<T> Intercept<T>(this ISignalInterceptingPipelineBuilder<T> builder,
        Func<T, InterceptionResult> intercept)
        where T : Signal
    {
        return builder.Intercept((signal, _) => intercept(signal));
    }
}
