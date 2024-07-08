using Talkie.Bridges.Telegram.Models;

namespace Talkie.Bridges.Telegram.Clients;

public static partial class TelegramBotApiClientExtensions
{
    public static Task<Update[]> GetUpdatesAsync(this ITelegramBotApiClient client, GetUpdates getUpdates,
        CancellationToken cancellationToken = default)
    {
        return client.SendAsync<Update[], GetUpdates>("getUpdates", getUpdates, cancellationToken);
    }

    public static Task<User> GetMeAsync(this ITelegramBotApiClient client,
        CancellationToken cancellationToken = default)
    {
        return client.SendAsync<User>("getMe", cancellationToken);
    }

    public static Task<Message> SendMessageAsync(this ITelegramBotApiClient client, SendMessage message,
        CancellationToken cancellationToken = default)
    {
        return client.SendAsync<Message, SendMessage>("sendMessage", message, cancellationToken);
    }
}
