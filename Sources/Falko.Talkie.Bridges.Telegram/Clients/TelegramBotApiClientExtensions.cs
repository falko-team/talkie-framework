using Talkie.Bridges.Telegram.Models;
using File = Talkie.Bridges.Telegram.Models.File;

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

    public static Task<Message> SendMessageAsync(this ITelegramBotApiClient client, SendMessage request,
        CancellationToken cancellationToken = default)
    {
        return client.SendAsync<Message, SendMessage>("sendMessage", request, cancellationToken);
    }

    public static Task<bool> DeleteMessageAsync(this ITelegramBotApiClient client, DeleteMessage request,
        CancellationToken cancellationToken = default)
    {
        return client.SendAsync<bool, DeleteMessage>("deleteMessage", request, cancellationToken);
    }

    public static Task<Message> EditMessageTextAsync(this ITelegramBotApiClient client, EditMessageText request,
        CancellationToken cancellationToken = default)
    {
        return client.SendAsync<Message, EditMessageText>("editMessageText", request, cancellationToken);
    }

    public static Task<File> GetFileAsync(this ITelegramBotApiClient client, GetFile request,
        CancellationToken cancellationToken = default)
    {
        return client.SendAsync<File, GetFile>("getFile", request, cancellationToken);
    }
}
