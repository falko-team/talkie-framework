namespace Talkie.Models.Messages.Contents.Styles;

public sealed record UnderlineTextStyle(int Offset, int Length) : IMessageTextStyle
{
    public IMessageTextStyle MutateTextRange(int offset, int length) => FromTextRange((offset, length));

    public static UnderlineTextStyle FromTextRange(MessageTextRange range) => new(range.Offset, range.Length);
}
