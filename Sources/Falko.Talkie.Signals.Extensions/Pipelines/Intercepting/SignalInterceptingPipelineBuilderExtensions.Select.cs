using Falko.Talkie.Interceptors;
using Falko.Talkie.Signals;

namespace Falko.Talkie.Pipelines.Intercepting;

public static partial class SignalInterceptingPipelineBuilderExtensions
{
    public static ISignalInterceptingPipelineBuilder Select
    (
        this ISignalInterceptingPipelineBuilder builder,
        Func<Signal, CancellationToken, Signal> select
    )
    {
        return builder.InterceptSingleton(() => new SelectSignalInterceptor(select));
    }

    public static ISignalInterceptingPipelineBuilder Select
    (
        this ISignalInterceptingPipelineBuilder builder,
        Func<Signal, Signal> select
    )
    {
        return builder.Select((signal, _) => select(signal));
    }

    public static ISignalInterceptingPipelineBuilder<TTo> Select<TFrom, TTo>
    (
        this ISignalInterceptingPipelineBuilder<TFrom> builder,
        Func<TFrom, CancellationToken, TTo> select
    )
        where TFrom : Signal
        where TTo : Signal
    {
        return builder
            .InterceptSingleton(() => new SelectSignalInterceptor<TFrom, TTo>(select))
            .OfDynamic()
            .OfType<TTo>();
    }

    public static ISignalInterceptingPipelineBuilder<TTo> Select<TFrom, TTo>
    (
        this ISignalInterceptingPipelineBuilder<TFrom> builder,
        Func<TFrom, TTo> select
    )
        where TFrom : Signal
        where TTo : Signal
    {
        return builder.Select<TFrom, TTo>((signal, _) => select(signal));
    }
}
