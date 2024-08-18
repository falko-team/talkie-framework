namespace Talkie.Models.Messages.Contents;

public sealed record MonospaceTextStyle(int Offset, int Length) : IMessageTextStyle
{
    public static MonospaceTextStyle FromContext(MessageTextContext context)
    {
        return new MonospaceTextStyle(context.Offset, context.Length);
    }
}
