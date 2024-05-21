using Talkie.Connections;
using Talkie.Flows;

namespace Talkie.Connectors;

public sealed class TelegramSignalConnector(string token) : ISignalConnector
{
    public ISignalConnection Connect(ISignalFlow flow)
    {
        return new TelegramSignalConnection(flow, token);
    }
}
