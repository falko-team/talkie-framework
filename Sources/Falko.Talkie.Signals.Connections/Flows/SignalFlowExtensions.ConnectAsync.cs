using Falko.Talkie.Connectors;

namespace Falko.Talkie.Flows;

public static partial class SignalFlowExtensions
{
    public static async ValueTask<IAsyncDisposable> ConnectAsync
    (
        this ISignalFlow flow,
        ISignalConnector connector,
        CancellationToken cancellationToken = default
    )
    {
        var connection = connector.Connect(flow);

        await connection.InitializeAsync(cancellationToken);

        return connection;
    }
}
