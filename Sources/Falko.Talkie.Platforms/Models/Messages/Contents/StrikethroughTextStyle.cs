namespace Talkie.Models.Messages.Contents;

public sealed record StrikethroughTextStyle(int Offset, int Length) : IMessageTextStyle
{
    public static StrikethroughTextStyle FromContext(MessageTextContext context)
    {
        return new StrikethroughTextStyle(context.Offset, context.Length);
    }
}
