using Falko.Talkie.Concurrent;
using Falko.Talkie.Signals;

namespace Falko.Talkie.Flows;

public static partial class SignalFlowExtensions
{
    public static void Publish
    (
        this ISignalFlow flow,
        Signal signal,
        CancellationToken cancellationToken = default
    )
    {
        _ = flow
            .PublishAsync(signal, cancellationToken)
            .HandleOnFault(exception => HandlePublishingException(flow, exception, cancellationToken));
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
        Exception? exception,
        CancellationToken cancellationToken
    )
    {
        _ = flow.PublishUnobservedPublishingExceptionAsync
        (
            exception ?? new InvalidOperationException("Failed to publish message."),
            cancellationToken
        );
    }
}
