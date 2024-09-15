using Talkie.Models.Messages.Contents.Styles;

namespace Talkie.Models.Messages.Contents;

public interface IMessageContentBuilder
{
    int TextLength { get; }

    int StylesCount { get; }

    IMessageContentBuilder AddText(string separator, IEnumerable<string> tokens);

    IMessageContentBuilder AddText(char separator, IEnumerable<string> tokens);

    IMessageContentBuilder AddText(string token, int repeat);

    IMessageContentBuilder AddText(char token, int repeat);

    IMessageContentBuilder AddText(ReadOnlySpan<char> token, int repeat);

    IMessageContentBuilder AddText(ReadOnlyMemory<char> token, int repeat);

    IMessageContentBuilder AddText(char text);

    IMessageContentBuilder AddText(string text);

    IMessageContentBuilder AddText(ReadOnlySpan<char> text);

    IMessageContentBuilder AddText(ReadOnlyMemory<char> text);

    IMessageContentBuilder AddStyle(IMessageTextStyle style);

    MessageContent Build();
}
