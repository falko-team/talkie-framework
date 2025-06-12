namespace Falko.Talkie.Models.Messages.Contents.Styles;

public sealed record BoldTextStyle(int Offset, int Length) : IMessageTextStyle
{
    public IMessageTextStyle MutateTextRange(Func<MessageTextRange, MessageTextRange> rangeMutator)
    {
        return FromTextRange(rangeMutator(this.ToTextRange()));
    }

    public static BoldTextStyle FromTextRange(MessageTextRange range) => new(range.Offset, range.Length);
}
