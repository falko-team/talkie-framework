namespace Falko.Talkie.Models.Messages.Contents.Styles;

public static partial class MessageTextStyleExtensions
{
    public static IMessageTextStyle MutateTextRangeOffset(this IMessageTextStyle style, Func<int, int> offsetMutator)
    {
        return style.MutateTextRange(range => new MessageTextRange(offsetMutator(range.Offset), range.Length));
    }

    public static IMessageTextStyle MutateTextRangeLength(this IMessageTextStyle style, Func<int, int> lengthMutator)
    {
        return style.MutateTextRange(range => new MessageTextRange(range.Offset, lengthMutator(range.Length)));
    }
}
