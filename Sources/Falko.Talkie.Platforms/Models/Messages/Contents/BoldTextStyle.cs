namespace Talkie.Models.Messages.Contents;

public sealed record BoldTextStyle(int Offset, int Length) : IMessageTextStyle
{
    public static BoldTextStyle FromRange(MessageTextRange range) => new(range.Offset, range.Length);
}
