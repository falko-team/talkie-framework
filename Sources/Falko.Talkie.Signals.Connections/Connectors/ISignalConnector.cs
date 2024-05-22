using Talkie.Connections;
using Talkie.Flows;

namespace Talkie.Connectors;

public interface ISignalConnector
{
    ISignalConnection Connect(ISignalFlow flow);
}
