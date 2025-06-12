using Falko.Talkie.Bridges.Telegram.Configurations;
using Falko.Talkie.Connections;
using Falko.Talkie.Flows;

namespace Falko.Talkie.Connectors;

public sealed class TelegramSignalConnector(TelegramConfiguration configuration) : ISignalConnector
{
    public ISignalConnection Connect(ISignalFlow flow)
    {
        ArgumentNullException.ThrowIfNull(flow);

        return new TelegramSignalConnection(flow, configuration);
    }
}
