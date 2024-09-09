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

    public static Task<bool> DeleteMessageAsync(this ITelegramBotApiClient client, DeleteMessage deleteMessage,
        CancellationToken cancellationToken = default)
    {
        return client.SendAsync<bool, DeleteMessage>("deleteMessage", deleteMessage, cancellationToken);
    }

    public static Task<Message> EditMessageTextAsync(this ITelegramBotApiClient client, EditMessageText editMessageText,
        CancellationToken cancellationToken = default)
    {
        return client.SendAsync<Message, EditMessageText>("editMessageText", editMessageText, cancellationToken);
    }
}
