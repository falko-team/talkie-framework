using Falko.Talkie.Connections;
using Falko.Talkie.Flows;

namespace Falko.Talkie.Connectors;

public sealed class TelegramSignalConnector(string token) : ISignalConnector
{
    public ISignalConnection Connect(ISignalFlow flow)
    {
        return new TelegramSignalConnection(flow, token);
    }
}
