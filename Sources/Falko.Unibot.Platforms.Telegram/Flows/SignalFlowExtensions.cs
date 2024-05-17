using Falko.Unibot.Connections;
using Falko.Unibot.Connectors;

namespace Falko.Unibot.Flows;

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
