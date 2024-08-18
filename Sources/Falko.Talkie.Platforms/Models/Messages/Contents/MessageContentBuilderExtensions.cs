namespace Talkie.Models.Messages.Contents;

public static partial class MessageContentBuilderExtensions
{
    public static IMessageContentBuilder AddText(this IMessageContentBuilder builder, string text,
        params Func<MessageTextContext, IMessageTextStyle>[] styleFactories)
    {
        builder.AddText(text);

        var styleContext = new MessageTextContext(builder.TextLength - text.Length, text.Length);

        foreach (var style in styleFactories.Select(factory => factory(styleContext)))
        {
            builder.AddTextStyle(style);
        }

        return builder;
    }

    public static IMessageContentBuilder AddTextLine(this IMessageContentBuilder builder, string text)
    {
        return builder.AddText(text).AddTextLine();
    }

    public static IMessageContentBuilder AddTextLine(this IMessageContentBuilder builder, string text,
        params Func<MessageTextContext, IMessageTextStyle>[] styleFactories)
    {
        return builder.AddText(text, styleFactories).AddTextLine();
    }

    public static IMessageContentBuilder AddTextLine(this IMessageContentBuilder builder)
    {
        return builder.AddText("\n");
    }
}
