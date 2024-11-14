namespace Talkie.Bridges.Telegram.Configurations;

public sealed record TelegramServerConfiguration(string Token)
{
    public string Domain { get; init; } = "api.telegram.org";

    public TimeSpan DefaultRetryDelay { get; init; } = TimeSpan.FromSeconds(30);

    public static implicit operator TelegramServerConfiguration(string token) => new(token);
}
