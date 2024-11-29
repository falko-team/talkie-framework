using Microsoft.Extensions.Logging;
using Talkie.Disposables;
using Talkie.Flows;
using Talkie.Pipelines.Handling;
using Talkie.Pipelines.Intercepting;

namespace Talkie.Subscribers;

internal sealed class UnobservedExceptionsLoggingSubscriber
(
    ILogger<UnobservedExceptionsShutdownSubscriber> logger
) : IBehaviorsSubscriber
{
    private static readonly string UnobservedExceptionLogMessage =
        "Unobserved exception occurred in the signal flow";

    public void Subscribe(ISignalFlow flow, IRegisterOnlyDisposableScope disposables, CancellationToken cancellationToken)
    {
        flow.Subscribe(signals => signals
            .Where(signal => signal.IsUnobservedExceptionSignal())
            .Handle(context => logger
                .LogError(context.Signal.GetUnobservedExceptionSignalException(),
                    UnobservedExceptionLogMessage)))
            .UnsubscribeWith(disposables);
    }
}
