namespace Talkie.Models.Messages.Contents;

public sealed record ItalicTextStyle(int Offset, int Length) : IMessageTextStyle
{
    public static ItalicTextStyle FromRange(MessageTextRange range) => new(range.Offset, range.Length);
}
