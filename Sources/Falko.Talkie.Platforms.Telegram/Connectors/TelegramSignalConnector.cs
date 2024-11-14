using Talkie.Bridges.Telegram.Configurations;
using Talkie.Connections;
using Talkie.Flows;

namespace Talkie.Connectors;

public sealed class TelegramSignalConnector(TelegramServerConfiguration serverConfiguration,
    TelegramClientConfiguration clientConfiguration) : ISignalConnector
{
    public ISignalConnection Connect(ISignalFlow flow)
    {
        return new TelegramSignalConnection(flow, serverConfiguration, clientConfiguration);
    }
}
