using Talkie.Models.Messages.Contents.Styles;

namespace Talkie.Models.Messages.Contents;

public static partial class MessageContentBuilderExtensions
{
    public static IMessageContentBuilder AddTextLine(this IMessageContentBuilder builder, string text)
    {
        return builder.AddText(text).AddTextLine();
    }

    public static IMessageContentBuilder AddTextLine(this IMessageContentBuilder builder, ReadOnlyMemory<char> text)
    {
        return builder.AddText(text).AddTextLine();
    }

    public static IMessageContentBuilder AddTextLine(this IMessageContentBuilder builder, ReadOnlySpan<char> text)
    {
        return builder.AddText(text).AddTextLine();
    }

    public static IMessageContentBuilder AddTextLine(this IMessageContentBuilder builder, char text,
        params Func<MessageTextRange, IMessageTextStyle>[] styleFactories)
    {
        return builder.AddText(text, styleFactories).AddTextLine();
    }

    public static IMessageContentBuilder AddTextLine(this IMessageContentBuilder builder, string text,
        params Func<MessageTextRange, IMessageTextStyle>[] styleFactories)
    {
        return builder.AddText(text, styleFactories).AddTextLine();
    }

    public static IMessageContentBuilder AddTextLine(this IMessageContentBuilder builder, ReadOnlyMemory<char> text,
        params Func<MessageTextRange, IMessageTextStyle>[] styleFactories)
    {
        return builder.AddText(text, styleFactories).AddTextLine();
    }

    public static IMessageContentBuilder AddTextLine(this IMessageContentBuilder builder, ReadOnlySpan<char> text,
        params Func<MessageTextRange, IMessageTextStyle>[] styleFactories)
    {
        return builder.AddText(text, styleFactories).AddTextLine();
    }

    public static IMessageContentBuilder AddTextLine(this IMessageContentBuilder builder)
    {
        return builder.AddText("\n");
    }
}
