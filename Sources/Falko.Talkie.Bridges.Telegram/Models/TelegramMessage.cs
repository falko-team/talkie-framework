namespace Talkie.Bridges.Telegram.Models;

public sealed class TelegramMessage
(
    long messageId,
    long? messageThreadId = null,
    TelegramUser? from = null,
    TelegramChat? senderChat = null,
    int? senderBoostCount = null,
    TelegramUser? senderBusinessBot = null,
    DateTime? date = null,
    string? businessConnectionId = null,
    TelegramChat? chat = null,
    bool? isTopicMessage = null,
    bool? isAutomaticForward = null,
    TelegramMessage? replyToMessage = null,
    TelegramUser? viaBot = null,
    string? text = null,
    IReadOnlyCollection<TelegramMessageEntity>? entities = null,
    string? caption = null,
    IReadOnlyCollection<TelegramMessageEntity>? captionEntities = null,
    string? mediaGroupId = null,
    IReadOnlyCollection<TelegramPhotoSize>? photo = null,
    TelegramSticker? sticker = null
)
{
    public long MessageId => messageId;

    public long? MessageThreadId => messageThreadId;

    public TelegramUser? From => from;

    public TelegramChat? SenderChat => senderChat;

    public int? SenderBoostCount => senderBoostCount;

    public TelegramUser? SenderBusinessBot => senderBusinessBot;

    public DateTime? Date => date;

    public string? BusinessConnectionId => businessConnectionId;

    public TelegramChat? Chat => chat;

    public bool? IsTopicMessage => isTopicMessage;

    public bool? IsAutomaticForward => isAutomaticForward;

    public TelegramMessage? ReplyToMessage => replyToMessage;

    public TelegramUser? ViaBot => viaBot;

    public string? Text => text;

    public IReadOnlyCollection<TelegramMessageEntity>? Entities => entities;

    public string? Caption => caption;

    public IReadOnlyCollection<TelegramMessageEntity>? CaptionEntities => captionEntities;

    public string? MediaGroupId => mediaGroupId;

    public IReadOnlyCollection<TelegramPhotoSize>? Photo => photo;

    public TelegramSticker? Sticker => sticker;
}
