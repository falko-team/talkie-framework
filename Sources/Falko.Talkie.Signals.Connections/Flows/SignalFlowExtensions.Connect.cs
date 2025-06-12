using Falko.Talkie.Connections;
using Falko.Talkie.Connectors;

namespace Falko.Talkie.Flows;

public static partial class SignalFlowExtensions
{
    public static ISignalConnection Connect(this ISignalFlow flow, ISignalConnector connector)
    {
        return connector.Connect(flow);
    }
}
