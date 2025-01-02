using Talkie.Signals;

namespace Talkie.Flows;

public static partial class SignalFlowExtensions
{
    public static void Publish
    (
        this ISignalFlow flow,
        Signal signal,
        CancellationToken cancellationToken = default
    )
    {
        _ = flow.PublishAsync(signal, cancellationToken)
            .ContinueWith(task => HandlePublishingException(flow, task, cancellationToken),
                TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.OnlyOnFaulted);
    }

    public static void Publish<T>
    (
        this ISignalFlow flow,
        CancellationToken cancellationToken = default
    ) where T : Signal, new()
    {
        flow.Publish(SignalCache<T>.Instance, cancellationToken);
    }

    private static void HandlePublishingException
    (
        ISignalFlow flow,
        Task task,
        CancellationToken cancellationToken
    )
    {
        if (task.IsFaulted is false || cancellationToken.IsCancellationRequested) return;

        Exception exception = task.Exception is { } taskException
            ? taskException
            : new InvalidOperationException("Failed to publish message.");

        _ = flow.PublishUnobservedPublishingExceptionAsync(exception, cancellationToken);
    }
}
