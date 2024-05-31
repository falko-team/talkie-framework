using Talkie.Pipelines;
using Talkie.Signals;

namespace Talkie.Flows;

public static partial class SignalFlowExtensions
{
    public static async Task<Signal> TakeAsync(this ISignalFlow flow,
        Func<ISignalInterceptingPipelineBuilder, ISignalInterceptingPipelineBuilder> pipelineFactory,
        CancellationToken cancellationToken = default)
    {
        var taskCompletionSource = new TaskCompletionSource<Signal>();

        var subscription = flow.Subscribe(signals => pipelineFactory(signals)
            .Handle(context => taskCompletionSource
                .SetResult(context.Signal)));

        cancellationToken.Register(() => taskCompletionSource.TrySetCanceled());

        try
        {
            return await taskCompletionSource.Task;
        }
        finally
        {
            subscription.Remove();
        }
    }

    public static async Task<T> TakeAsync<T>(this ISignalFlow flow,
        Func<ISignalInterceptingPipelineBuilder, ISignalInterceptingPipelineBuilder<T>> pipelineFactory,
        CancellationToken cancellationToken = default) where T : Signal
    {
        var taskCompletionSource = new TaskCompletionSource<T>();

        var subscription = flow.Subscribe(signals => pipelineFactory(signals)
            .Handle(context => taskCompletionSource
                .SetResult(context.Signal)));

        cancellationToken.Register(() => taskCompletionSource.TrySetCanceled());

        try
        {
            return await taskCompletionSource.Task;
        }
        finally
        {
            subscription.Remove();
        }
    }

    public static async Task<TTo> TakeAsync<TFrom, TTo>(this ISignalFlow flow,
        Func<ISignalInterceptingPipelineBuilder<TFrom>, ISignalInterceptingPipelineBuilder<TTo>> pipelineFactory,
        CancellationToken cancellationToken = default) where TFrom : Signal where TTo : Signal
    {
        var taskCompletionSource = new TaskCompletionSource<TTo>();

        var subscription = flow.Subscribe<TFrom>(signals => pipelineFactory(signals)
            .Handle(context => taskCompletionSource
                .SetResult(context.Signal)));

        cancellationToken.Register(() => taskCompletionSource.TrySetCanceled());

        try
        {
            return await taskCompletionSource.Task;
        }
        finally
        {
            subscription.Remove();
        }
    }

    public static async Task<T> TakeAsync<T>(this ISignalFlow flow, CancellationToken cancellationToken = default) where T : Signal
    {
        return await flow.TakeAsync<T>(signals => signals.OfType<T>(), cancellationToken);
    }

    public static async Task<Signal> TakeAsync(this ISignalFlow flow, CancellationToken cancellationToken = default)
    {
        return await flow.TakeAsync(signals => signals, cancellationToken);
    }
}
