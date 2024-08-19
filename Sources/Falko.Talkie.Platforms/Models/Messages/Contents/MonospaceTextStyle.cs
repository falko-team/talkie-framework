namespace Talkie.Models.Messages.Contents;

public sealed record MonospaceTextStyle(int Offset, int Length) : IMessageTextStyle
{
    public static MonospaceTextStyle FromRange(MessageTextRange range) => new(range.Offset, range.Length);
}
