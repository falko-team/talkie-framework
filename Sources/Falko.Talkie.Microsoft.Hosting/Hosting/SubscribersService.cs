using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Talkie.Concurrent;
using Talkie.Disposables;
using Talkie.Flows;
using Talkie.Sequences;
using Talkie.Subscribers;

namespace Talkie.Hosting;

internal sealed class SubscribersService
(
    ISignalFlow flow,
    IEnumerable<IBehaviorsSubscriber> behaviorsSubscribers,
    IEnumerable<IIntegrationsSubscriber> integrationsSubscribers,
    ILogger<SubscribersService> logger
) : IHostedService
{
    private readonly ReverseDisposableScope _disposables = new();

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogDebug("Subscribing subscribers to the signal flow");

        try
        {
            var behaviorDisposables = new ParallelDisposableScope()
                .DisposeAsyncWith(_disposables);

            foreach (var behaviorsSubscriber in behaviorsSubscribers)
            {
                cancellationToken.ThrowIfCancellationRequested();

                SubscribeBehaviors
                (
                    behaviorsSubscriber,
                    behaviorDisposables,
                    cancellationToken
                );
            }

            var integrationsDisposables = new ParallelDisposableScope()
                .DisposeAsyncWith(_disposables);

            await integrationsSubscribers
                .ToFrozenSequence()
                .Parallelize()
                .ForEachAsync((integrationsSubscriber, cancellation) => SubscribeIntegrations
                    (
                        integrationsSubscriber,
                        integrationsDisposables,
                        cancellation
                    ),
                    cancellationToken);
        }
        catch
        {
            await StopAsync(cancellationToken);

            throw;
        }

        logger.LogDebug("Subscribed subscribers to the signal flow");
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogDebug("Unsubscribing subscriptions of the signal flow");

        await _disposables.DisposeAsync();

        logger.LogDebug("Unsubscribed subscriptions of the signal flow");
    }

    private void SubscribeBehaviors(IBehaviorsSubscriber subscriber,
        IRegisterOnlyDisposableScope disposables,
        CancellationToken cancellationToken)
    {
        logger.LogTrace("Subscribing behaviors of {Subscriber} to the signal flow", subscriber);

        subscriber.Subscribe(flow, disposables, cancellationToken);

        logger.LogTrace("Subscribed behaviors of {Subscriber} to the signal flow", subscriber);
    }

    private async ValueTask SubscribeIntegrations(IIntegrationsSubscriber subscriber,
        IRegisterOnlyDisposableScope disposables,
        CancellationToken cancellationToken)
    {
        logger.LogTrace("Subscribing integrations of {Subscriber} to the signal flow", subscriber);

        await subscriber.SubscribeAsync(flow, disposables, cancellationToken);

        logger.LogTrace("Subscribed integrations of {Subscriber} to the signal flow", subscriber);
    }
}
