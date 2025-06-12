namespace Falko.Talkie.Models.Messages.Contents.Styles;

public interface IMessageTextStyle
{
    int Offset { get; }

    int Length { get; }

    IMessageTextStyle MutateTextRange(Func<MessageTextRange, MessageTextRange> rangeMutator);

    public static IMessageTextStyle FromTextRange(MessageTextRange range) => throw new NotImplementedException();
}
