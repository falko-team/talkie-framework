using Falko.Talkie.Disposables;
using Falko.Talkie.Flows;
using Falko.Talkie.Pipelines.Handling;
using Microsoft.Extensions.Logging;

namespace Falko.Talkie.Subscribers;

internal sealed class SignalsLoggingSubscriber(ILogger<SignalsLoggingSubscriber> logger) : IBehaviorsSubscriber
{
    public void Subscribe(ISignalFlow flow, IRegisterOnlyDisposableScope disposables, CancellationToken cancellationToken)
    {
        flow.Subscribe(signals => signals
            .Handle(context => logger
                .LogTrace("Handling signal: {Signal}", context.Signal)))
            .UnsubscribeWith(disposables);
    }
}
