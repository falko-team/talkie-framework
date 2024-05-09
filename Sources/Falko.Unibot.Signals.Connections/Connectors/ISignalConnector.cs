using Falko.Unibot.Connections;
using Falko.Unibot.Flows;

namespace Falko.Unibot.Connectors;

public interface ISignalConnector
{
    ISignalConnection Connect(ISignalFlow flow);
}
