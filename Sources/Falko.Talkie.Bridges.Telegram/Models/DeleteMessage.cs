namespace Talkie.Bridges.Telegram.Models;

public sealed class DeleteMessage
(
    long messageId,
    long chatId
)
{
    public readonly long MessageId = messageId;

    public readonly long ChatId = chatId;
}
