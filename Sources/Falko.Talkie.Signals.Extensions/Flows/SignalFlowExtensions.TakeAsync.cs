using Talkie.Pipelines;
using Talkie.Signals;

namespace Talkie.Flows;

public static partial class SignalFlowExtensions
{
    public static async Task<Signal> TakeAsync(this ISignalFlow flow,
        Func<ISignalInterceptingPipelineBuilder, ISignalInterceptingPipelineBuilder> pipelineFactory,
        CancellationToken cancellationToken = default)
    {
        var taskSource = new TaskCompletionSource<Signal>();

        SafeSetCancellationForTaskSource(taskSource, cancellationToken);

        var subscription = flow.Subscribe(signals => pipelineFactory(signals)
            .Handle((context, cancellation) => SafeSetResultForTaskSource(taskSource, context.Signal, cancellation)));

        try
        {
            return await taskSource.Task;
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
        var taskSource = new TaskCompletionSource<T>();

        SafeSetCancellationForTaskSource(taskSource, cancellationToken);

        var subscription = flow.Subscribe(signals => pipelineFactory(signals)
            .Handle((context, cancellation) => SafeSetResultForTaskSource(taskSource, context.Signal, cancellation)));

        try
        {
            return await taskSource.Task;
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
        var taskSource = new TaskCompletionSource<TTo>();

        SafeSetCancellationForTaskSource(taskSource, cancellationToken);

        var subscription = flow.Subscribe<TFrom>(signals => pipelineFactory(signals)
            .Handle((context, cancellation) => SafeSetResultForTaskSource(taskSource, context.Signal, cancellation)));

        try
        {
            return await taskSource.Task;
        }
        finally
        {
            subscription.Remove();
        }
    }

    public static async Task<T> TakeAsync<T>(this ISignalFlow flow,
        CancellationToken cancellationToken = default) where T : Signal
    {
        return await flow.TakeAsync<T>(signals => signals.OfType<T>(), cancellationToken);
    }

    public static async Task<Signal> TakeAsync(this ISignalFlow flow,
        CancellationToken cancellationToken = default)
    {
        return await flow.TakeAsync(signals => signals, cancellationToken);
    }

    private static void SafeSetCancellationForTaskSource<T>(TaskCompletionSource<T> source,
        CancellationToken cancellationToken) where T : Signal
    {
        if (cancellationToken == CancellationToken.None || cancellationToken.CanBeCanceled is false)
        {
            return;
        }

        cancellationToken.Register(() =>
        {
            try
            {
                source.TrySetCanceled();
            }
            catch
            {
                // Nothing to do here
            }
        });
    }

    private static void SafeSetResultForTaskSource<T>(TaskCompletionSource<T> source,
        T result,
        CancellationToken cancellationToken) where T : Signal
    {
        try
        {
            if (cancellationToken.IsCancellationRequested)
            {
                source.TrySetCanceled();

                return;
            }

            source.TrySetResult(result);
        }
        catch
        {
            // Nothing to do here
        }
    }
}
