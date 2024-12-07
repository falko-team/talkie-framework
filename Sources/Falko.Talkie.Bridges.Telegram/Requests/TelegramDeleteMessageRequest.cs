namespace Talkie.Bridges.Telegram.Requests;

public sealed class TelegramDeleteMessageRequest
(
    long messageId,
    long chatId
)
{
    public long MessageId => messageId;

    public long ChatId => chatId;
}
