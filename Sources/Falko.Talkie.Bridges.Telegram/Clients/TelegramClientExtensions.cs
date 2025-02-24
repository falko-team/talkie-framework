using Talkie.Bridges.Telegram.Models;
using Talkie.Bridges.Telegram.Requests;
using Talkie.Sequences;

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
            methodName: "getUpdates",
            request: getUpdates,
            cancellationToken: cancellationToken
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
            methodName: "getMe",
            cancellationToken: cancellationToken
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
            methodName: "sendMessage",
            request: request,
            cancellationToken: cancellationToken
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
            methodName: "deleteMessage",
            request: request,
            cancellationToken: cancellationToken
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
            methodName: "editMessageText",
            request: request,
            cancellationToken: cancellationToken
        );
    }

    public static Task<TelegramMessage> SendPhotoAsync
    (
        this ITelegramClient client,
        TelegramSendPhotoRequest request,
        FrozenSequence<TelegramStream> streams,
        CancellationToken cancellationToken = default
    )
    {
        return client.SendAsync<TelegramMessage, TelegramSendPhotoRequest>
        (
            methodName: "sendPhoto",
            request: request,
            streams: streams,
            cancellationToken: cancellationToken
        );
    }

    public static Task<IReadOnlyList<TelegramMessage>> SendMediaGroupAsync
    (
        this ITelegramClient client,
        TelegramSendMediaGroupRequest request,
        FrozenSequence<TelegramStream> streams,
        CancellationToken cancellationToken = default
    )
    {
        return client.SendAsync<IReadOnlyList<TelegramMessage>, TelegramSendMediaGroupRequest>
        (
            methodName: "sendMediaGroup",
            request: request,
            streams: streams,
            cancellationToken: cancellationToken
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
            methodName: "getFile",
            request: request,
            cancellationToken: cancellationToken
        );
    }
}
