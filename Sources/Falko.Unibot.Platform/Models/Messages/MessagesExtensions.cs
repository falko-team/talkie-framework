namespace Falko.Unibot.Models.Messages;

public static class MessagesExtensions
{
    public static XMessage.IWithEntry? WithEntry(this IMessage model)
    {
        return model as XMessage.IWithEntry;
    }
}
