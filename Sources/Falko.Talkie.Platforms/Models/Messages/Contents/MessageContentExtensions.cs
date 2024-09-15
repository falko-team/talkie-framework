using Talkie.Models.Messages.Contents.Styles;

namespace Talkie.Models.Messages.Contents;

public static partial class MessageContentExtensions
{
    public static MessageContent MutateText(this MessageContent content,
        Func<string, string> textMutationFactory)
    {
        return content with { Text = textMutationFactory(content.Text) };
    }

    public static MessageContent MutateStyles(this MessageContent content,
        Func<IReadOnlyCollection<IMessageTextStyle>, IReadOnlyCollection<IMessageTextStyle>> stylesMutationFactory)
    {
        return content with { Styles = stylesMutationFactory(content.Styles) };
    }
}
