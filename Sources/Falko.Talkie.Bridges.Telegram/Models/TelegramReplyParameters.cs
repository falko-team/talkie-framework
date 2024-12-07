namespace Talkie.Bridges.Telegram.Models;

public sealed class TelegramReplyParameters
(
    long messageId,
    long? chatId = null
)
{
    public long MessageId => messageId;

    public long? ChatId => chatId;
}
