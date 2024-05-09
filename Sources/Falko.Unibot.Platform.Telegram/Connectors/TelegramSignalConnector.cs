using Falko.Unibot.Connections;
using Falko.Unibot.Flows;

namespace Falko.Unibot.Connectors;

public sealed class TelegramSignalConnector(string token) : ISignalConnector
{
    public ISignalConnection Connect(ISignalFlow flow)
    {
        return new TelegramSignalConnection(flow, token);
    }
}
