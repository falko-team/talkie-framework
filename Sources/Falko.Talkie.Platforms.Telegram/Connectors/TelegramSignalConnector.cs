using Talkie.Bridges.Telegram.Configurations;
using Talkie.Connections;
using Talkie.Flows;

namespace Talkie.Connectors;

public sealed class TelegramSignalConnector(TelegramConfiguration configuration) : ISignalConnector
{
    public ISignalConnection Connect(ISignalFlow flow)
    {
        ArgumentNullException.ThrowIfNull(flow);

        return new TelegramSignalConnection(flow, configuration);
    }
}
