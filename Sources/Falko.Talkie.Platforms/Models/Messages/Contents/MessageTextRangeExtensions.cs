using Talkie.Models.Messages.Contents.Styles;

namespace Talkie.Models.Messages.Contents;

public static class MessageTextRangeExtensions
{
    public static MessageTextRange ToTextRange(this IMessageTextStyle style)
    {
        return new MessageTextRange(style.Offset, style.Length);
    }

    public static ReadOnlySpan<char> GetTextRange(this MessageContent content, MessageTextRange textRange)
    {
        if (textRange.Offset >= content.Text.Length) return ReadOnlySpan<char>.Empty;

        var length = Math.Min(textRange.Length, content.Text.Length - textRange.Offset);

        if (length <= 0) return ReadOnlySpan<char>.Empty;

        return content.Text.AsSpan(textRange.Offset, length);
    }

    public static ReadOnlySpan<char> GetTextRange(this MessageContent content, IMessageTextStyle style)
    {
        return content.GetTextRange(style.ToTextRange());
    }
}
