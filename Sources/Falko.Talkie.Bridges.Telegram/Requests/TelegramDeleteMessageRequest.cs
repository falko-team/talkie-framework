namespace Falko.Talkie.Bridges.Telegram.Requests;

public sealed class TelegramDeleteMessageRequest
(
    long messageId,
    long chatId
) : ITelegramRequest<bool>
{
    public long MessageId => messageId;

    public long ChatId => chatId;
}
