namespace Falko.Talkie.Models.Messages.Contents.Styles;

public sealed record LinkTextStyle(int Offset, int Length, string Link) : IMessageTextStyle
{
    public IMessageTextStyle MutateTextRange(Func<MessageTextRange, MessageTextRange> rangeMutator)
    {
        return FromLink(Link).FromTextRange(rangeMutator(this.ToTextRange()));
    }

    public static LinkTextStyleBuilder FromLink(string link) => new(link);
}
