using Falko.Unibot.Connections;
using Falko.Unibot.Connectors;

namespace Falko.Unibot.Flows;

public static partial class SignalFlowExtensions
{
    public static ISignalConnection Connect(this ISignalFlow flow, ISignalConnector connector)
    {
        return connector.Connect(flow);
    }

    public static async ValueTask<IAsyncDisposable> ConnectAsync(this ISignalFlow flow, ISignalConnector connector,
        CancellationToken cancellationToken = default)
    {
        var connection = connector.Connect(flow);

        await connection.InitializeAsync(cancellationToken);

        return connection;
    }
}
