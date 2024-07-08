using Talkie.Connections;
using Talkie.Connectors;

namespace Talkie.Flows;

public static partial class SignalFlowExtensions
{
    public static ISignalConnection Connect(this ISignalFlow flow, ISignalConnector connector)
    {
        return connector.Connect(flow);
    }
}
