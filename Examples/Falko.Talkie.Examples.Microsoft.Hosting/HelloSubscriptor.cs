using Talkie.Concurrent;
using Talkie.Controllers.MessageControllers;
using Talkie.Disposables;
using Talkie.Flows;
using Talkie.Handlers;
using Talkie.Models.Messages;
using Talkie.Pipelines.Handling;
using Talkie.Pipelines.Intercepting;
using Talkie.Signals;
using Talkie.Subscribers;

namespace Talkie.Examples;

public sealed class HelloSubscriptor : IBehaviorsSubscriber
{
    public void Subscribe(ISignalFlow flow, IRegisterOnlyDisposableScope disposables, CancellationToken cancellationToken)
    {
        flow.Subscribe<MessagePublishedSignal>(static signals => signals
            .SkipSelf()
            .SkipOlderThan(TimeSpan
                .FromMinutes(1))
            .Where(signal => signal
                .Message
                .GetText()
                .TrimStart()
                .StartsWith("/hello", StringComparison.InvariantCultureIgnoreCase))
            .HandleAsync((context, cancellationToken) => context
                .ToMessageController()
                .PublishMessageAsync("Hi!", cancellationToken)
                .AsValueTask())
            .Handle(_ => throw new Exception()))
            .UnsubscribeWith(disposables);
    }
}
