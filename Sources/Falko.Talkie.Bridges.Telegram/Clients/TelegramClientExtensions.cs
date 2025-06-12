using Falko.Talkie.Bridges.Telegram.Models;
using Falko.Talkie.Bridges.Telegram.Policies;
using Falko.Talkie.Bridges.Telegram.Requests;
using Falko.Talkie.Sequences;

namespace Falko.Talkie.Bridges.Telegram.Clients;

public static partial class TelegramClientExtensions
{
    public static Task<IReadOnlyList<TelegramUpdate>> GetUpdatesAsync
    (
        this ITelegramClient client,
        TelegramGetUpdatesRequest getUpdates,
        ITelegramRetryPolicy? policy = null,
        CancellationToken cancellationToken = default
    )
    {
        return client.SendRequestAsync<IReadOnlyList<TelegramUpdate>, TelegramGetUpdatesRequest>
        (
            methodName: "getUpdates",
            request: getUpdates,
            policy: policy,
            cancellationToken: cancellationToken
        );
    }

    public static Task<TelegramUser> GetMeAsync
    (
        this ITelegramClient client,
        ITelegramRetryPolicy? policy = null,
        CancellationToken cancellationToken = default
    )
    {
        return client.SendRequestAsync<TelegramUser>
        (
            methodName: "getMe",
            policy: policy,
            cancellationToken: cancellationToken
        );
    }

    public static Task<TelegramMessage> SendMessageAsync
    (
        this ITelegramClient client,
        TelegramSendMessageRequest request,
        ITelegramRetryPolicy? policy = null,
        CancellationToken cancellationToken = default
    )
    {
        return client.SendRequestAsync<TelegramMessage, TelegramSendMessageRequest>
        (
            methodName: "sendMessage",
            request: request,
            policy: policy,
            cancellationToken: cancellationToken
        );
    }

    public static Task<bool> DeleteMessageAsync
    (
        this ITelegramClient client,
        TelegramDeleteMessageRequest request,
        ITelegramRetryPolicy? policy = null,
        CancellationToken cancellationToken = default
    )
    {
        return client.SendRequestAsync<bool, TelegramDeleteMessageRequest>
        (
            methodName: "deleteMessage",
            request: request,
            policy: policy,
            cancellationToken: cancellationToken
        );
    }

    public static Task<TelegramMessage> EditMessageTextAsync
    (
        this ITelegramClient client,
        TelegramEditMessageTextRequest request,
        ITelegramRetryPolicy? policy = null,
        CancellationToken cancellationToken = default
    )
    {
        return client.SendRequestAsync<TelegramMessage, TelegramEditMessageTextRequest>
        (
            methodName: "editMessageText",
            request: request,
            policy: policy,
            cancellationToken: cancellationToken
        );
    }

    public static Task<TelegramMessage> SendPhotoAsync
    (
        this ITelegramClient client,
        TelegramSendPhotoRequest request,
        TelegramStream stream = default,
        ITelegramRetryPolicy? policy = null,
        CancellationToken cancellationToken = default
    )
    {
        return client.SendRequestAsync<TelegramMessage, TelegramSendPhotoRequest>
        (
            methodName: "sendPhoto",
            request: request,
            streams: stream == default
                ? FrozenSequence.Empty<TelegramStream>()
                : stream,
            policy: policy,
            cancellationToken: cancellationToken
        );
    }

    public static Task<TelegramMessage> SendAudioAsync
    (
        this ITelegramClient client,
        TelegramSendAudioRequest request,
        TelegramStream audioStream = default,
        TelegramStream thumbnailStream = default,
        ITelegramRetryPolicy? policy = null,
        CancellationToken cancellationToken = default
    )
    {
        return client.SendRequestAsync<TelegramMessage, TelegramSendAudioRequest>
        (
            methodName: "sendAudio",
            request: request,
            streams: FrozenSequence
                .Wrap(audioStream, thumbnailStream)
                .Where(stream => stream != default)
                .ToFrozenSequence(),
            policy: policy,
            cancellationToken: cancellationToken
        );
    }

    public static Task<TelegramMessage> SendStickerAsync
    (
        this ITelegramClient client,
        TelegramSendStickerRequest request,
        ITelegramRetryPolicy? policy = null,
        CancellationToken cancellationToken = default
    )
    {
        return client.SendRequestAsync<TelegramMessage, TelegramSendStickerRequest>
        (
            methodName: "sendSticker",
            request: request,
            policy: policy,
            cancellationToken: cancellationToken
        );
    }

    public static Task<IReadOnlyList<TelegramMessage>> SendMediaGroupAsync
    (
        this ITelegramClient client,
        TelegramSendMediaGroupRequest request,
        FrozenSequence<TelegramStream> streams,
        ITelegramRetryPolicy? policy = null,
        CancellationToken cancellationToken = default
    )
    {
        return client.SendRequestAsync<IReadOnlyList<TelegramMessage>, TelegramSendMediaGroupRequest>
        (
            methodName: "sendMediaGroup",
            request: request,
            streams: streams,
            policy: policy,
            cancellationToken: cancellationToken
        );
    }

    public static Task<TelegramFile> GetFileAsync
    (
        this ITelegramClient client,
        TelegramGetFileRequest request,
        ITelegramRetryPolicy? policy = null,
        CancellationToken cancellationToken = default
    )
    {
        return client.SendRequestAsync<TelegramFile, TelegramGetFileRequest>
        (
            methodName: "getFile",
            request: request,
            policy: policy,
            cancellationToken: cancellationToken
        );
    }
}
