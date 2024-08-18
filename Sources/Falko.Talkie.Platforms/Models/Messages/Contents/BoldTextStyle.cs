namespace Talkie.Models.Messages.Contents;

public sealed record BoldTextStyle(int Offset, int Length) : IMessageTextStyle
{
    public static BoldTextStyle FromRange(MessageTextRange range)
    {
        return new BoldTextStyle(range.Offset, range.Length);
    }
}
