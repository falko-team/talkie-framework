namespace Talkie.Models.Messages.Contents;

public sealed record BoldTextStyle(int Offset, int Length) : IMessageTextStyle
{
    public static BoldTextStyle FromContext(MessageTextContext context)
    {
        return new BoldTextStyle(context.Offset, context.Length);
    }
}
