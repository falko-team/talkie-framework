namespace Talkie.Bridges.Telegram.Requests;

public sealed class TelegramDeleteMessageRequest
(
    long messageId,
    long chatId
)
{
    public readonly long MessageId = messageId;

    public readonly long ChatId = chatId;
}
