using Talkie.Bridges.Telegram.Configurations;
using Talkie.Connections;
using Talkie.Connectors;

namespace Talkie.Flows;

public static partial class SignalFlowExtensions
{
    public static ISignalConnection ConnectTelegram
    (
        this ISignalFlow flow,
        TelegramConfiguration configuration
    )
    {
        return flow.Connect(new TelegramSignalConnector(configuration));
    }

    public static ISignalConnection ConnectTelegram
    (
        this ISignalFlow flow,
        TelegramServerConfiguration configuration
    )
    {
        return flow.ConnectTelegram(new TelegramConfiguration(configuration));
    }

    public static ValueTask<IAsyncDisposable> ConnectTelegramAsync
    (
        this ISignalFlow flow,
        TelegramConfiguration configuration,
        CancellationToken cancellationToken = default
    )
    {
        return flow.ConnectAsync(new TelegramSignalConnector(configuration), cancellationToken);
    }

    public static ValueTask<IAsyncDisposable> ConnectTelegramAsync
    (
        this ISignalFlow flow,
        TelegramServerConfiguration configuration,
        CancellationToken cancellationToken = default
    )
    {
        return flow.ConnectTelegramAsync(new TelegramConfiguration(configuration), cancellationToken);
    }
}
