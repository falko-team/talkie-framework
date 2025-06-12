using Falko.Talkie.Interceptors;
using Falko.Talkie.Signals;

namespace Falko.Talkie.Pipelines.Intercepting;

public static partial class SignalInterceptingPipelineBuilderExtensions
{
    public static ISignalInterceptingPipelineBuilder<TSignal> DistinctUntilChanged<TSignal, TValue>
    (
        this ISignalInterceptingPipelineBuilder<TSignal> builder,
        Func<TSignal, CancellationToken, TValue> distinctUntilChanged
    ) where TSignal : Signal
    {
        return builder.InterceptTransient(() => new DistinctUntilChangedSignalInterceptor<TSignal, TValue>(distinctUntilChanged));
    }

    public static ISignalInterceptingPipelineBuilder DistinctUntilChanged<TValue>
    (
        this ISignalInterceptingPipelineBuilder builder,
        Func<Signal, CancellationToken, TValue> distinctUntilChanged
    )
    {
        return builder.InterceptTransient(() => new DistinctUntilChangedSignalInterceptor<TValue>(distinctUntilChanged));
    }

    public static ISignalInterceptingPipelineBuilder<TSignal> DistinctUntilChanged<TSignal, TValue>
    (
        this ISignalInterceptingPipelineBuilder<TSignal> builder,
        Func<TSignal, TValue> distinctUntilChanged
    ) where TSignal : Signal
    {
        return builder.DistinctUntilChanged((signal, _) => distinctUntilChanged(signal));
    }

    public static ISignalInterceptingPipelineBuilder DistinctUntilChanged<TValue>
    (
        this ISignalInterceptingPipelineBuilder builder,
        Func<Signal, TValue> distinctUntilChanged
    )
    {
        return builder.DistinctUntilChanged((signal, _) => distinctUntilChanged(signal));
    }

    public static ISignalInterceptingPipelineBuilder<TSignal> DistinctUntilChanged<TSignal>
    (
        this ISignalInterceptingPipelineBuilder<TSignal> builder
    ) where TSignal : Signal
    {
        return builder.DistinctUntilChanged((signal, _) => signal);
    }

    public static ISignalInterceptingPipelineBuilder DistinctUntilChanged
    (
        this ISignalInterceptingPipelineBuilder builder
    )
    {
        return builder.DistinctUntilChanged((signal, _) => signal);
    }
}
