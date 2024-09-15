namespace Talkie.Models.Messages.Contents.Styles;

public interface IMessageTextStyle
{
    int Offset { get; }

    int Length { get; }

    IMessageTextStyle MutateTextRange(int offset, int length);

    public static IMessageTextStyle FromTextRange(MessageTextRange range) => throw new NotImplementedException();
}
