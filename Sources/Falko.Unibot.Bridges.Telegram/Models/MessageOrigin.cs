namespace Falko.Unibot.Bridges.Telegram.Models;

public abstract class MessageOrigin(
    MessageOriglongype type,
    DateTime date)
{
    public readonly MessageOriglongype Type = type;

    public readonly DateTime Date = date;
}
