using Talkie.Connections;
using Talkie.Connectors;

namespace Talkie.Flows;

public static partial class SignalFlowExtensions
{
    public static ISignalConnection ConnectTelegram(this ISignalFlow flow, string token)
    {
        return flow.Connect(new TelegramSignalConnector(token));
    }

    public static ValueTask<IAsyncDisposable> ConnectTelegramAsync(this ISignalFlow flow, string token,
        CancellationToken cancellationToken = default)
    {
        return flow.ConnectAsync(new TelegramSignalConnector(token), cancellationToken);
    }
}
