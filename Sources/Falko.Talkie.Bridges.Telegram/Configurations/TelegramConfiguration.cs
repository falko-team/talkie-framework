namespace Talkie.Bridges.Telegram.Configurations;

public readonly struct TelegramConfiguration
{
    public readonly TelegramServerConfiguration ServerConfiguration;

    public readonly TelegramClientConfiguration ClientConfiguration;

    public TelegramConfiguration
    (
        TelegramServerConfiguration serverConfiguration,
        TelegramClientConfiguration? clientConfiguration = null
    )
    {
        ArgumentNullException.ThrowIfNull(serverConfiguration);

        ServerConfiguration = serverConfiguration;
        ClientConfiguration = clientConfiguration ?? new TelegramClientConfiguration();
    }

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
