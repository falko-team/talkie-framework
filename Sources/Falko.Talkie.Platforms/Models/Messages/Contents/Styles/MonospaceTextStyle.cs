namespace Talkie.Models.Messages.Contents.Styles;

public sealed record MonospaceTextStyle(int Offset, int Length) : IMessageTextStyle
{
    public IMessageTextStyle MutateTextRange(int offset, int length) => FromTextRange((offset, length));

    public static MonospaceTextStyle FromTextRange(MessageTextRange range) => new(range.Offset, range.Length);
}
