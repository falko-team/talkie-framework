using Talkie.Models.Messages.Contents.Styles;

namespace Talkie.Models.Messages.Contents;

public static partial class MessageContentBuilderExtensions
{
    private const char NewLine = '\n';

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
        params IReadOnlyCollection<Func<MessageTextRange, IMessageTextStyle>> styleFactories)
    {
        return builder.AddText(text, styleFactories).AddTextLine();
    }

    public static IMessageContentBuilder AddTextLine(this IMessageContentBuilder builder, string text,
        params IReadOnlyCollection<Func<MessageTextRange, IMessageTextStyle>> styleFactories)
    {
        return builder.AddText(text, styleFactories).AddTextLine();
    }

    public static IMessageContentBuilder AddTextLine(this IMessageContentBuilder builder, ReadOnlyMemory<char> text,
        params IReadOnlyCollection<Func<MessageTextRange, IMessageTextStyle>> styleFactories)
    {
        return builder.AddText(text, styleFactories).AddTextLine();
    }

    public static IMessageContentBuilder AddTextLine(this IMessageContentBuilder builder, ReadOnlySpan<char> text,
        params IReadOnlyCollection<Func<MessageTextRange, IMessageTextStyle>> styleFactories)
    {
        return builder.AddText(text, styleFactories).AddTextLine();
    }

    public static IMessageContentBuilder AddTextLine(this IMessageContentBuilder builder)
    {
        return builder.AddText(NewLine);
    }
}
