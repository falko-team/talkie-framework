using Talkie.Common;
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

public sealed class ExBehaviour : IBehaviorsSubscriber
{
    public void Subscribe(ISignalFlow flow, IRegisterOnlyDisposableScope disposables, CancellationToken cancellationToken)
    {
        var s = ConcurrentExclusiveSchedulerPair

        flow.Subscribe(signals => signals
            .OfType<UserAcceptInvoice>()
            .SkipSelfPublished()
            .Where(signal => signal.Message.GetText().IsNullOrEmpty() is false)
            .HandleAsync(async context =>
            {
                Console.WriteLine("Incoming message with text");

                await context.ToMessageController().PublishMessageAsync("Hello");
            }))
            .UnsubscribeWith(disposables);
    }
}
