using Talkie.Models.Messages.Contents.Styles;

namespace Talkie.Models.Messages;

public static class MessageExtensions
{
    public static string GetText(this IMessage message)
    {
        return message.Content.Text;
    }

    public static IReadOnlyCollection<IMessageTextStyle> GetStyles(this IMessage message)
    {
        return message.Content.Styles;
    }
}
