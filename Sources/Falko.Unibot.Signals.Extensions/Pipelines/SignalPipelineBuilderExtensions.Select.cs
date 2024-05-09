using Falko.Unibot.Interceptors;
using Falko.Unibot.Signals;

namespace Falko.Unibot.Pipelines;

public static partial class SignalPipelineBuilderExtensions
{
    public static ISignalInterceptingPipelineBuilder Select(this ISignalInterceptingPipelineBuilder builder,
        Func<Signal, CancellationToken, Signal> select)
    {
        return builder.Intercept(new SelectSignalInterceptor(select));
    }

    public static ISignalInterceptingPipelineBuilder Select(this ISignalInterceptingPipelineBuilder builder,
        Func<Signal, Signal> select)
    {
        return builder.Select((signal, _) => select(signal));
    }

    public static ISignalInterceptingPipelineBuilder<TTo> Select<TFrom, TTo>(this ISignalInterceptingPipelineBuilder<TFrom> builder,
            Func<TFrom, CancellationToken, TTo> select)
        where TFrom : Signal
        where TTo : Signal
    {
        return new SignalInterceptingPipelineBuilder<TTo>(builder
            .Intercept(new SelectSignalInterceptor<TFrom, TTo>(select))
            .ToInterceptors());
    }

    public static ISignalInterceptingPipelineBuilder<TTo> Select<TFrom, TTo>(this ISignalInterceptingPipelineBuilder<TFrom> builder,
            Func<TFrom, TTo> select)
        where TFrom : Signal
        where TTo : Signal
    {
        return builder.Select((signal, _) => select(signal));
    }
}
