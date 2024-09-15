namespace Talkie.Models.Messages.Contents.Styles;

public sealed record ItalicTextStyle(int Offset, int Length) : IMessageTextStyle
{
    public IMessageTextStyle MutateTextRange(Func<MessageTextRange, MessageTextRange> rangeMutator)
    {
        return FromTextRange(rangeMutator(this.ToTextRange()));
    }

    public static ItalicTextStyle FromTextRange(MessageTextRange range) => new(range.Offset, range.Length);
}
