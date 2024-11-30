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

public sealed class HelloSubscriber : IBehaviorsSubscriber
{
    public void Subscribe(ISignalFlow flow, IRegisterOnlyDisposableScope disposables, CancellationToken cancellationToken)
    {
        flow.Subscribe<MessagePublishedSignal>(static signals => signals
            .SkipSelfPublished()
            .SkipOlderThan(TimeSpan.FromMinutes(1))
            .InterceptOn(Schedulers.Intercepting.Random())
            .Do(_ => Thread.Sleep(1000))
            .InterceptOn(Schedulers.Intercepting.Current())
            .Where(signal => signal
                .Message
                .GetText()
                .TrimStart()
                .StartsWith("/hello", StringComparison.InvariantCultureIgnoreCase))
            .HandleAsync((context, cancellationToken) => context
                .ToMessageController()
                .PublishMessageAsync("Hi!", cancellationToken)
                .AsValueTask()))
            .UnsubscribeWith(disposables);
    }
}
