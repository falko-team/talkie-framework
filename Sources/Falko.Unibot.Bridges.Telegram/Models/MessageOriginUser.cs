namespace Falko.Unibot.Bridges.Telegram.Models;

public sealed class MessageOriginUser(
    MessageOriglongype type,
    DateTime date,
    User senderUser) : MessageOrigin(type, date)
{
    public readonly User SenderUser = senderUser;
}
