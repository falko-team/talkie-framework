namespace Talkie.Bridges.Telegram.Models;

public sealed class TelegramReplyParameters
(
    long messageId,
    long? chatId = null
)
{
    public readonly long MessageId = messageId;

    public readonly long? ChatId = chatId;
}
