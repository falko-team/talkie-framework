namespace Talkie.Models.Messages.Contents;

public sealed record StrikethroughTextStyle(int Offset, int Length) : IMessageTextStyle
{
    public static StrikethroughTextStyle FromRange(MessageTextRange range)
    {
        return new StrikethroughTextStyle(range.Offset, range.Length);
    }
}
