namespace Talkie.Models.Messages.Contents;

public sealed record ItalicTextStyle(int Offset, int Length) : IMessageTextStyle
{
    public static ItalicTextStyle FromContext(MessageTextContext context)
    {
        return new ItalicTextStyle(context.Offset, context.Length);
    }
}
