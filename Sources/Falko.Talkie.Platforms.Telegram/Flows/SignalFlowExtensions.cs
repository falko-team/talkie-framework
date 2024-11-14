using Talkie.Bridges.Telegram.Configurations;
using Talkie.Connections;
using Talkie.Connectors;

namespace Talkie.Flows;

public static partial class SignalFlowExtensions
{
    public static ISignalConnection ConnectTelegram(this ISignalFlow flow,
        TelegramServerConfiguration serverConfiguration,
        TelegramClientConfiguration? clientConfiguration = default)
    {
        clientConfiguration ??= new TelegramClientConfiguration();

        return flow.Connect(new TelegramSignalConnector(serverConfiguration, clientConfiguration));
    }

    public static ValueTask<IAsyncDisposable> ConnectTelegramAsync(this ISignalFlow flow,
        TelegramServerConfiguration serverConfiguration,
        TelegramClientConfiguration? clientConfiguration = default,
        CancellationToken cancellationToken = default)
    {
        clientConfiguration ??= new TelegramClientConfiguration();

        return flow.ConnectAsync(new TelegramSignalConnector(serverConfiguration, clientConfiguration), cancellationToken);
    }

    public static ValueTask<IAsyncDisposable> ConnectTelegramAsync(this ISignalFlow flow,
        TelegramServerConfiguration serverConfiguration,
        CancellationToken cancellationToken)
    {
        return flow.ConnectTelegramAsync(serverConfiguration, default, cancellationToken);
    }
}
