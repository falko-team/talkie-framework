using Falko.Talkie.Disposables;
using Falko.Talkie.Flows;
using Falko.Talkie.Subscribers;
using Microsoft.Extensions.Configuration;

namespace Falko.Talkie.Examples;

public sealed class TelegramSubscriber(IConfiguration configuration) : IIntegrationsSubscriber
{
    public Task SubscribeAsync(ISignalFlow flow, IRegisterOnlyDisposableScope disposables, CancellationToken cancellationToken)
    {
        return flow.ConnectTelegramAsync(GetTelegramToken(), cancellationToken)
            .DisposeAsyncWith(disposables)
            .AsTask();
    }

    private string GetTelegramToken()
    {
        return configuration["Telegram:Token"]
            ?? throw new InvalidOperationException("Telegram token is not configured.");
    }
}
