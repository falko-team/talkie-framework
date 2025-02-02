using Talkie.Bridges.Telegram.Models;
using Talkie.Bridges.Telegram.Requests;

namespace Talkie.Bridges.Telegram.Clients;

public static partial class TelegramClientExtensions
{
    public static Task<IReadOnlyList<TelegramUpdate>> GetUpdatesAsync
    (
        this ITelegramClient client,
        TelegramGetUpdatesRequest getUpdates,
        CancellationToken cancellationToken = default
    )
    {
        return client.SendAsync<IReadOnlyList<TelegramUpdate>, TelegramGetUpdatesRequest>
        (
            "getUpdates",
            getUpdates,
            cancellationToken
        );
    }

    public static Task<TelegramUser> GetMeAsync
    (
        this ITelegramClient client,
        CancellationToken cancellationToken = default
    )
    {
        return client.SendAsync<TelegramUser>
        (
            "getMe",
            cancellationToken
        );
    }

    public static Task<TelegramMessage> SendMessageAsync
    (
        this ITelegramClient client,
        TelegramSendMessageRequest request,
        CancellationToken cancellationToken = default
    )
    {
        return client.SendAsync<TelegramMessage, TelegramSendMessageRequest>
        (
            "sendMessage",
            request,
            cancellationToken
        );
    }

    public static Task<bool> DeleteMessageAsync
    (
        this ITelegramClient client,
        TelegramDeleteMessageRequest request,
        CancellationToken cancellationToken = default
    )
    {
        return client.SendAsync<bool, TelegramDeleteMessageRequest>
        (
            "deleteMessage",
            request,
            cancellationToken
        );
    }

    public static Task<TelegramMessage> EditMessageTextAsync
    (
        this ITelegramClient client,
        TelegramEditMessageTextRequest request,
        CancellationToken cancellationToken = default
    )
    {
        return client.SendAsync<TelegramMessage, TelegramEditMessageTextRequest>
        (
            "editMessageText",
            request,
            cancellationToken
        );
    }

    public static Task<TelegramMessage> SendPhotoAsync
    (
        this ITelegramClient client,
        TelegramSendPhotoRequest request,
        CancellationToken cancellationToken = default
    )
    {
        return client.SendAsync<TelegramMessage, TelegramSendPhotoRequest>
        (
            "sendPhoto",
            request,
            cancellationToken
        );
    }

    public static Task<IReadOnlyList<TelegramMessage>> SendMediaGroupAsync
    (
        this ITelegramClient client,
        TelegramSendMediaGroupRequest request,
        CancellationToken cancellationToken = default
    )
    {
        return client.SendAsync<IReadOnlyList<TelegramMessage>, TelegramSendMediaGroupRequest>
        (
            "sendMediaGroup",
            request,
            cancellationToken
        );
    }

    public static Task<TelegramFile> GetFileAsync
    (
        this ITelegramClient client,
        TelegramGetFileRequest request,
        CancellationToken cancellationToken = default
    )
    {
        return client.SendAsync<TelegramFile, TelegramGetFileRequest>
        (
            "getFile",
            request,
            cancellationToken
        );
    }
}
