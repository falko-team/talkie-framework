using Falko.Talkie.Connections;
using Falko.Talkie.Connectors;

namespace Falko.Talkie.Flows;

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
