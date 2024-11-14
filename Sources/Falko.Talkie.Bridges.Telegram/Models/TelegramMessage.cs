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
    string? text = null,
    IReadOnlyCollection<TelegramMessageEntity>? entities = null,
    string? caption = null,
    IReadOnlyCollection<TelegramMessageEntity>? captionEntities = null,
    string? mediaGroupId = null,
    IReadOnlyCollection<TelegramPhotoSize>? photo = null,
    TelegramSticker? sticker = null
)
{
    public readonly long MessageId = messageId;

    public readonly long? MessageThreadId = messageThreadId;

    public readonly TelegramUser? From = from;

    public readonly TelegramChat? SenderChat = senderChat;

    public readonly int? SenderBoostCount = senderBoostCount;

    public readonly TelegramUser? SenderBusinessBot = senderBusinessBot;

    public readonly DateTime? Date = date;

    public readonly string? BusinessConnectionId = businessConnectionId;

    public readonly TelegramChat? Chat = chat;

    public readonly bool? IsTopicMessage = isTopicMessage;

    public readonly bool? IsAutomaticForward = isAutomaticForward;

    public readonly TelegramMessage? ReplyToMessage = replyToMessage;

    public readonly string? Text = text;

    public readonly IReadOnlyCollection<TelegramMessageEntity>? Entities = entities;

    public readonly string? Caption = caption;

    public readonly IReadOnlyCollection<TelegramMessageEntity>? CaptionEntities = captionEntities;

    public readonly string? MediaGroupId = mediaGroupId;

    public readonly IReadOnlyCollection<TelegramPhotoSize>? Photo = photo;

    public readonly TelegramSticker? Sticker = sticker;
}
