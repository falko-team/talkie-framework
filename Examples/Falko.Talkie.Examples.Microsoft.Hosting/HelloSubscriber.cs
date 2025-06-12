using Falko.Talkie.Concurrent;
using Falko.Talkie.Controllers.MessageControllers;
using Falko.Talkie.Disposables;
using Falko.Talkie.Flows;
using Falko.Talkie.Models.Messages;
using Falko.Talkie.Pipelines.Handling;
using Falko.Talkie.Pipelines.Intercepting;
using Falko.Talkie.Signals;
using Falko.Talkie.Subscribers;

namespace Falko.Talkie.Examples;

public sealed class HelloSubscriber : IBehaviorsSubscriber
{
    public void Subscribe(ISignalFlow flow, IRegisterOnlyDisposableScope disposables, CancellationToken cancellationToken)
    {
        flow.Subscribe<MessagePublishedSignal>(static signals => signals
            .SkipSelfRelated()
            .SkipOlderThan(TimeSpan
                .FromMinutes(1))
            .Where(signal => signal
                .Message
                .GetText()
                .TrimStart()
                .StartsWith("/hello", StringComparison.InvariantCultureIgnoreCase))
            .HandleAsync((context, cancellationToken) => MessageControllerExtensions.PublishMessageAsync(context
                    .ToMessageController(), "Hi!", cancellationToken)
                .AsValueTask()))
            .UnsubscribeWith(disposables);
    }
}
