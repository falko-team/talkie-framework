namespace Talkie.Bridges.Telegram.Models;

public sealed class Message(
    long messageId,
    long? messageThreadId = null,
    User? from = null,
    Chat? senderChat = null,
    int? senderBoostCount = null,
    User? senderBusinessBot = null,
    DateTime? date = null,
    string? businessConnectionId = null,
    Chat? chat = null,
    bool? isTopicMessage = null,
    bool? isAutomaticForward = null,
    Message? replyToMessage = null,
    string? text = null,
    IReadOnlyCollection<MessageEntity>? entities = null)
{
    public readonly long MessageId = messageId;

    public readonly long? MessageThreadId = messageThreadId;

    public readonly User? From = from;

    public readonly Chat? SenderChat = senderChat;

    public readonly int? SenderBoostCount = senderBoostCount;

    public readonly User? SenderBusinessBot = senderBusinessBot;

    public readonly DateTime? Date = date;

    public readonly string? BusinessConnectionId = businessConnectionId;

    public readonly Chat? Chat = chat;

    public readonly bool? IsTopicMessage = isTopicMessage;

    public readonly bool? IsAutomaticForward = isAutomaticForward;

    public readonly Message? ReplyToMessage = replyToMessage;

    public readonly string? Text = text;

    public readonly IReadOnlyCollection<MessageEntity>? Entities = entities;
}
