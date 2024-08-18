namespace Talkie.Models.Messages.Contents;

public interface IMessageContentBuilder
{
    int TextLength { get; }

    int StylesCount { get; }

    IMessageContentBuilder AddText(string text);

    IMessageContentBuilder AddTextStyle(IMessageTextStyle style);

    MessageContent Build();
}
