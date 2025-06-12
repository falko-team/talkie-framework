using Falko.Talkie.Validation;

namespace Falko.Talkie.Bridges.Telegram.Configurations;

public readonly struct TelegramConfiguration
(
    TelegramServerConfiguration serverConfiguration,
    TelegramClientConfiguration? clientConfiguration = null
)
{
    public TelegramServerConfiguration ServerConfiguration => Assert
        .ArgumentNullException.ThrowIfNull(serverConfiguration, nameof(serverConfiguration));

    public TelegramClientConfiguration ClientConfiguration =>
        clientConfiguration ?? new TelegramClientConfiguration();

    public void ThrowIfInvalid()
    {
        ArgumentNullException.ThrowIfNull(ServerConfiguration);
        ArgumentNullException.ThrowIfNull(ClientConfiguration);
    }

    public static implicit operator TelegramConfiguration(TelegramServerConfiguration serverConfiguration)
    {
        return new TelegramConfiguration(serverConfiguration);
    }
}
