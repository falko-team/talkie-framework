using Talkie.Bridges.Telegram.Configurations;
using Talkie.Connections;
using Talkie.Connectors;

namespace Talkie.Flows;

public static partial class SignalFlowExtensions
{
    public static ISignalConnection ConnectTelegram(this ISignalFlow flow,
        ServerConfiguration serverConfiguration,
        ClientConfiguration? clientConfiguration = default)
    {
        clientConfiguration ??= new ClientConfiguration();

        return flow.Connect(new TelegramSignalConnector(serverConfiguration, clientConfiguration));
    }

    public static ValueTask<IAsyncDisposable> ConnectTelegramAsync(this ISignalFlow flow,
        ServerConfiguration serverConfiguration,
        ClientConfiguration? clientConfiguration = default,
        CancellationToken cancellationToken = default)
    {
        clientConfiguration ??= new ClientConfiguration();

        return flow.ConnectAsync(new TelegramSignalConnector(serverConfiguration, clientConfiguration), cancellationToken);
    }
}
