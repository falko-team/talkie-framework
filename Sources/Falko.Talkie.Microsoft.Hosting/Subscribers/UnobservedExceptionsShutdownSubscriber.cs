using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Talkie.Disposables;
using Talkie.Flows;
using Talkie.Pipelines.Handling;
using Talkie.Pipelines.Intercepting;

namespace Talkie.Subscribers;

internal sealed class UnobservedExceptionsShutdownSubscriber
(
    IHostApplicationLifetime lifetime,
    ILogger<UnobservedExceptionsShutdownSubscriber> logger
) : IBehaviorsSubscriber
{
    public void Subscribe(ISignalFlow flow, IRegisterOnlyDisposableScope disposables, CancellationToken cancellationToken)
    {
        flow.Subscribe(signals => signals
            .Where(signal => signal.IsUnobservedExceptionSignal())
            .Take(1)
            .Handle(context => logger.LogCritical
            (
                context.Signal.GetUnobservedExceptionSignalException(),
                "Unobserved exception occurred in the signal flow that will shutdown the application"
            ))
            .Handle(lifetime.StopApplication))
            .UnsubscribeWith(disposables);
    }
}
