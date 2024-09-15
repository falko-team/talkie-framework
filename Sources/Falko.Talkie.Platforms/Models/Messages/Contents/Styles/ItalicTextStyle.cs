namespace Talkie.Models.Messages.Contents.Styles;

public sealed record ItalicTextStyle(int Offset, int Length) : IMessageTextStyle
{
    public IMessageTextStyle MutateTextRange(int offset, int length) => FromTextRange((offset, length));

    public static ItalicTextStyle FromTextRange(MessageTextRange range) => new(range.Offset, range.Length);
}
