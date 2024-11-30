using Microsoft.Extensions.Hosting;
using Talkie.Concurrent;
using Talkie.Controllers.MessageControllers;
using Talkie.Disposables;
using Talkie.Flows;
using Talkie.Handlers;
using Talkie.Hosting;
using Talkie.Models.Messages;
using Talkie.Pipelines.Handling;
using Talkie.Pipelines.Intercepting;
using Talkie.Signals;
using Talkie.Subscribers;

await new HostBuilder()
    .UseTalkie(configuration => configuration)
    .ConfigureServices(services => services
        .AddIntegrations<TelegramSubscriber>()
        .AddBehaviors<HelloWorldSubscriber>())
    .RunConsoleAsync();

file sealed class HelloWorldSubscriber : IBehaviorsSubscriber
{
    public void Subscribe
    (
        ISignalFlow flow,
        IRegisterOnlyDisposableScope disposables,
        CancellationToken cancellationToken
    )
    {
        flow.Subscribe<MessagePublishedSignal>(signals => signals
            .SkipSelfPublished()
            .SkipOlderThan(TimeSpan.FromMinutes(1))
            .Where(signal => signal
                .Message
                .GetText()
                .StartsWith("/hello", StringComparison.InvariantCultureIgnoreCase))
            .HandleAsync(context => context
                .ToMessageController()
                .PublishMessageAsync("Hi!", cancellationToken)
                .AsValueTask()))
            .UnsubscribeWith(disposables);
    }
}

file sealed class TelegramSubscriber : IIntegrationsSubscriber
{
    public async Task SubscribeAsync
    (
        ISignalFlow flow,
        IRegisterOnlyDisposableScope disposables,
        CancellationToken cancellationToken
    )
    {
        await flow.ConnectTelegramAsync("MY_TOKEN", cancellationToken)
            .DisposeAsyncWith(disposables);
    }
}
