namespace Talkie.Models.Messages.Contents.Styles;

public readonly struct LinkTextStyleBuilder(string link)
{
    public LinkTextStyle FromTextRange(MessageTextRange range) => new(range.Offset, range.Length, link);
}
