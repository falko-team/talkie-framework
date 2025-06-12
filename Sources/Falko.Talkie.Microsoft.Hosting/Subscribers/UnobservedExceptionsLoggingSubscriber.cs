using Falko.Talkie.Disposables;
using Falko.Talkie.Flows;
using Falko.Talkie.Pipelines.Handling;
using Falko.Talkie.Pipelines.Intercepting;
using Microsoft.Extensions.Logging;

namespace Falko.Talkie.Subscribers;

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
            .Where(signal => UnobservedExceptionSignalsExtensions.IsUnobservedExceptionSignal(signal))
            .Handle(context => logger
                .LogError(UnobservedExceptionSignalsExtensions.GetUnobservedExceptionSignalException(context.Signal),
                    UnobservedExceptionLogMessage)))
            .UnsubscribeWith(disposables);
    }
}
