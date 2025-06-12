namespace Falko.Talkie.Models.Messages.Contents.Styles;

public sealed record UnderlineTextStyle(int Offset, int Length) : IMessageTextStyle
{
    public IMessageTextStyle MutateTextRange(Func<MessageTextRange, MessageTextRange> rangeMutator)
    {
        return FromTextRange(rangeMutator(this.ToTextRange()));
    }

    public static UnderlineTextStyle FromTextRange(MessageTextRange range) => new(range.Offset, range.Length);
}
