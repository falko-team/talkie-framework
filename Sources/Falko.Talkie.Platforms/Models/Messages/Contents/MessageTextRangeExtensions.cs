using Falko.Talkie.Models.Messages.Contents.Styles;

namespace Falko.Talkie.Models.Messages.Contents;

public static class MessageTextRangeExtensions
{
    public static MessageTextRange ToTextRange(this IMessageTextStyle style)
    {
        return new MessageTextRange(style.Offset, style.Length);
    }

    public static ReadOnlySpan<char> GetTextRange(this MessageContent content, int offset, int length)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(offset, nameof(offset));
        ArgumentOutOfRangeException.ThrowIfNegative(length, nameof(length));

        if (offset >= content.Text.Length) return ReadOnlySpan<char>.Empty;

        length = Math.Min(length, content.Text.Length - offset);

        if (length <= 0) return ReadOnlySpan<char>.Empty;

        return content.Text.AsSpan(offset, length);
    }

    public static ReadOnlySpan<char> GetTextRange(this MessageContent content, MessageTextRange textRange)
    {
        return GetTextRange(content, textRange.Offset, textRange.Length);
    }

    public static ReadOnlySpan<char> GetTextRange(this MessageContent content, IMessageTextStyle style)
    {
        return content.GetTextRange(style.ToTextRange());
    }
}
