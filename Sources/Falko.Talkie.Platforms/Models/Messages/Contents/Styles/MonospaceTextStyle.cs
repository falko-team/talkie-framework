namespace Talkie.Models.Messages.Contents.Styles;

public sealed record MonospaceTextStyle(int Offset, int Length) : IMessageTextStyle
{
    public IMessageTextStyle MutateTextRange(Func<MessageTextRange, MessageTextRange> rangeMutator)
    {
        return FromTextRange(rangeMutator(this.ToTextRange()));
    }

    public static MonospaceTextStyle FromTextRange(MessageTextRange range) => new(range.Offset, range.Length);
}
