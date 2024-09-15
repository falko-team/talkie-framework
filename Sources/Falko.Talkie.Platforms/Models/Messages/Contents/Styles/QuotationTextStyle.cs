namespace Talkie.Models.Messages.Contents.Styles;

public sealed record QuotationTextStyle(int Offset, int Length) : IMessageTextStyle
{
    public IMessageTextStyle MutateTextRange(Func<MessageTextRange, MessageTextRange> rangeMutator)
    {
        return FromTextRange(rangeMutator(this.ToTextRange()));
    }

    public static QuotationTextStyle FromTextRange(MessageTextRange range) => new(range.Offset, range.Length);
}
