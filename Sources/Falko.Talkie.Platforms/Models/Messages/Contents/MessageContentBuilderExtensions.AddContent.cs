using Talkie.Models.Messages.Contents.Styles;

namespace Talkie.Models.Messages.Contents;

public static partial class MessageContentBuilderExtensions
{
    public static IMessageContentBuilder AddContent(this IMessageContentBuilder builder, MessageContent content)
    {
        if (content.IsEmpty) return builder;

        if (content.Styles.Count is 0) return builder.AddText(content.Text);

        AddStylesForText(builder, content.Styles);

        return builder.AddText(content.Text);
    }

    public static IMessageContentBuilder AddContent(this IMessageContentBuilder builder, MessageContent content,
        params Func<MessageTextRange, IMessageTextStyle>[] styleFactories)
    {
        if (content.IsEmpty) return builder;

        AddStylesForText(builder, content.Text.Length, styleFactories);

        if (content.Styles.Count is 0) return builder.AddText(content.Text);

        AddStylesForText(builder, content.Styles);

        return builder.AddText(content.Text);
    }

    private static void AddStylesForText(IMessageContentBuilder builder, IReadOnlyCollection<IMessageTextStyle> styles)
    {
        foreach (var style in styles)
        {
            builder.AddStyle(style.MutateTextRangeOffset(offset => offset + builder.TextLength));
        }
    }
}
