using Falko.Talkie.Disposables;
using Falko.Talkie.Flows;
using Falko.Talkie.Pipelines.Handling;
using Falko.Talkie.Pipelines.Intercepting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Falko.Talkie.Subscribers;

internal sealed class UnobservedExceptionsShutdownSubscriber
(
    IHostApplicationLifetime lifetime,
    ILogger<UnobservedExceptionsShutdownSubscriber> logger
) : IBehaviorsSubscriber
{
    public void Subscribe(ISignalFlow flow, IRegisterOnlyDisposableScope disposables, CancellationToken cancellationToken)
    {
        flow.Subscribe(signals => signals
            .Where(signal => UnobservedExceptionSignalsExtensions.IsUnobservedExceptionSignal(signal))
            .Take(1)
            .Handle(context => logger.LogCritical
            (
                UnobservedExceptionSignalsExtensions.GetUnobservedExceptionSignalException(context.Signal),
                "Unobserved exception occurred in the signal flow that will shutdown the application"
            ))
            .Handle(lifetime.StopApplication))
            .UnsubscribeWith(disposables);
    }
}
