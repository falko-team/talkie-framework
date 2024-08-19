namespace Talkie.Models.Messages.Contents;

public sealed record UnderlineTextStyle(int Offset, int Length) : IMessageTextStyle
{
    public static UnderlineTextStyle FromRange(MessageTextRange range) => new(range.Offset, range.Length);
}
