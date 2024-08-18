namespace Talkie.Models.Messages.Contents;

public sealed record MonospaceTextStyle(int Offset, int Length) : IMessageTextStyle
{
    public static MonospaceTextStyle FromRange(MessageTextRange range)
    {
        return new MonospaceTextStyle(range.Offset, range.Length);
    }
}
