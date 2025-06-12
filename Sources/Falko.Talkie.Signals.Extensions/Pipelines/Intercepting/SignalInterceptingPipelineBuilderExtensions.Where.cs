using Falko.Talkie.Interceptors;
using Falko.Talkie.Signals;

namespace Falko.Talkie.Pipelines.Intercepting;

public static partial class SignalInterceptingPipelineBuilderExtensions
{
    public static ISignalInterceptingPipelineBuilder<T> Where<T>
    (
        this ISignalInterceptingPipelineBuilder<T> builder,
        Func<T, CancellationToken, bool> where
    ) where T : Signal
    {
        return builder.InterceptSingleton(() => new WhereSignalInterceptor<T>(where));
    }

    public static ISignalInterceptingPipelineBuilder<T> Where<T>
    (
        this ISignalInterceptingPipelineBuilder<T> builder,
        Func<T, bool> where
    ) where T : Signal
    {
        return builder.Where((signal, _) => where(signal));
    }

    public static ISignalInterceptingPipelineBuilder Where
    (
        this ISignalInterceptingPipelineBuilder builder,
        Func<Signal, CancellationToken, bool> where
    )
    {
        return builder.InterceptSingleton(() => new WhereSignalInterceptor(where));
    }

    public static ISignalInterceptingPipelineBuilder Where
    (
        this ISignalInterceptingPipelineBuilder builder,
        Func<Signal, bool> where
    )
    {
        return builder.Where((signal, _) => where(signal));
    }
}
