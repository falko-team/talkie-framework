namespace Falko.Unibot.Bridges.Telegram.Models;

public sealed class MessageOriginHiddenUser(
    MessageOriglongype type,
    DateTime date,
    string senderUserName) : MessageOrigin(type, date)
{
    public readonly string SenderUserName = senderUserName;
}
