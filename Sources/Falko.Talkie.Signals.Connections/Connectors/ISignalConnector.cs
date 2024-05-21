using Falko.Talkie.Connections;
using Falko.Talkie.Flows;

namespace Falko.Talkie.Connectors;

public interface ISignalConnector
{
    ISignalConnection Connect(ISignalFlow flow);
}
