using Microsoft.Extensions.Logging;
using Talkie.Disposables;
using Talkie.Flows;
using Talkie.Pipelines.Handling;

namespace Talkie.Subscribers;

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
