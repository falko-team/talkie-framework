namespace Talkie.Models.Messages.Contents;

public sealed record ItalicTextStyle(int Offset, int Length) : IMessageTextStyle
{
    public static ItalicTextStyle FromRange(MessageTextRange range)
    {
        return new ItalicTextStyle(range.Offset, range.Length);
    }
}
