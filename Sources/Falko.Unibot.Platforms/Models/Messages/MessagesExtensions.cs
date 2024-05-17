using Falko.Unibot.Platforms;

namespace Falko.Unibot.Models.Messages;

public static class MessagesExtensions
{
    public static XMessage.IWithEntry? WithEntry(this IMessage model)
    {
        return model as XMessage.IWithEntry;
    }

    public static XMessage.IWithIdentifier? WithIdentifier(this IMessage model)
    {
        return model as XMessage.IWithIdentifier;
    }

    public static XMessage.IWithPlatform? WithPlatform(this IMessage model)
    {
        return model as XMessage.IWithPlatform;
    }

    public static bool OfPlatform<T>(this IMessage model) where T : class, IPlatform
    {
        return model.WithPlatform()?.Platform is T;
    }

    public static XMessage.IWithReply? WithReply(this IMessage model)
    {
        return model as XMessage.IWithReply;
    }
}
