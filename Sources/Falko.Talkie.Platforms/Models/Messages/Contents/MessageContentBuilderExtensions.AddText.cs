using Talkie.Models.Messages.Contents.Styles;

namespace Talkie.Models.Messages.Contents;

public static partial class MessageContentBuilderExtensions
{
    public static IMessageContentBuilder AddText(this IMessageContentBuilder builder, char text,
        params Func<MessageTextRange, IMessageTextStyle>[] styleFactories)
    {
        AddStylesForText(builder, 1, styleFactories);

        return builder.AddText(text);
    }

    public static IMessageContentBuilder AddText(this IMessageContentBuilder builder, string text,
        params Func<MessageTextRange, IMessageTextStyle>[] styleFactories)
    {
        AddStylesForText(builder, text.Length, styleFactories);

        return builder.AddText(text);
    }

    public static IMessageContentBuilder AddText(this IMessageContentBuilder builder, ReadOnlyMemory<char> text,
        params Func<MessageTextRange, IMessageTextStyle>[] styleFactories)
    {
        AddStylesForText(builder, text.Length, styleFactories);

        return builder.AddText(text);
    }

    public static IMessageContentBuilder AddText(this IMessageContentBuilder builder, ReadOnlySpan<char> text,
        params Func<MessageTextRange, IMessageTextStyle>[] styleFactories)
    {
        AddStylesForText(builder, text.Length, styleFactories);

        return builder.AddText(text);
    }

    public static IMessageContentBuilder AddText(this IMessageContentBuilder builder, string token, int repeat,
        params Func<MessageTextRange, IMessageTextStyle>[] styleFactories)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(repeat, nameof(repeat));

        if (repeat is 0) return builder;

        if (token.Length is 0) return builder;

        if (repeat is 1) return builder.AddText(token, styleFactories);

        AddStylesForText(builder, token.Length * repeat, styleFactories);

        return builder.AddText(token, repeat);
    }

    public static IMessageContentBuilder AddText(this IMessageContentBuilder builder, char token, int repeat,
        params Func<MessageTextRange, IMessageTextStyle>[] styleFactories)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(repeat, nameof(repeat));

        if (repeat is 0) return builder;

        if (repeat is 1) return builder.AddText(new ReadOnlySpan<char>(ref token), styleFactories);

        AddStylesForText(builder, repeat, styleFactories);

        return builder.AddText(token, repeat);
    }

    public static IMessageContentBuilder AddText(this IMessageContentBuilder builder, ReadOnlySpan<char> token, int repeat,
        params Func<MessageTextRange, IMessageTextStyle>[] styleFactories)
    {
        if (token.Length is 0) return builder;

        return builder.AddText(token.ToString(), repeat, styleFactories);
    }

    public static IMessageContentBuilder AddText(this IMessageContentBuilder builder, ReadOnlyMemory<char> token, int repeat,
        params Func<MessageTextRange, IMessageTextStyle>[] styleFactories)
    {
        if (token.Length is 0) return builder;

        return builder.AddText(token.ToString(), repeat, styleFactories);
    }

    public static IMessageContentBuilder AddText(this IMessageContentBuilder builder, string separator, IReadOnlyCollection<string> tokens,
        params Func<MessageTextRange, IMessageTextStyle>[] styleFactories)
    {
        if (separator.Length is 0) return builder;

        if (tokens.Count is 1) return builder.AddText(tokens.First(), styleFactories);

        AddStylesForText(builder, FindJoinLength(separator.Length, tokens), styleFactories);

        return builder.AddText(separator, tokens);
    }

    public static IMessageContentBuilder AddText(this IMessageContentBuilder builder, char separator, IReadOnlyCollection<string> tokens,
        params Func<MessageTextRange, IMessageTextStyle>[] styleFactories)
    {
        if (tokens.Count is 1) return builder.AddText(tokens.First(), styleFactories);

        AddStylesForText(builder, FindJoinLength(1, tokens), styleFactories);

        return builder.AddText(separator, tokens);
    }

    private static int FindJoinLength(int separatorLength, IReadOnlyCollection<string> tokens)
    {
        return tokens.Sum(token => token.Length) + separatorLength * (tokens.Count - 1);
    }

    private static void AddStylesForText(IMessageContentBuilder builder, int length,
        Func<MessageTextRange, IMessageTextStyle>[] styleFactories)
    {
        if (styleFactories.Length is 0) return;

        var styleContext = new MessageTextRange(builder.TextLength, length);

        foreach (var style in styleFactories.Select(factory => factory(styleContext)))
        {
            builder.AddStyle(style);
        }
    }
}
