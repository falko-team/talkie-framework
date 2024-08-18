namespace Talkie.Models.Messages.Contents;

public sealed record UnderlineTextStyle(int Offset, int Length) : IMessageTextStyle
{
    public static UnderlineTextStyle FromContext(MessageTextContext context)
    {
        return new UnderlineTextStyle(context.Offset, context.Length);
    }
}
