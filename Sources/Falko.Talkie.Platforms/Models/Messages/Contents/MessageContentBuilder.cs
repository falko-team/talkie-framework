using System.Text;
using Talkie.Models.Messages.Contents.Styles;
using Talkie.Sequences;

namespace Talkie.Models.Messages.Contents;

public sealed class MessageContentBuilder : IMessageContentBuilder
{
    private readonly StringBuilder _text = new();

    private readonly Sequence<IMessageTextStyle> _styles = new();

    public int TextLength => _text.Length;

    public int StylesCount => _styles.Count;

    public IMessageContentBuilder AddText(string separator, IEnumerable<string> tokens)
    {
        _text.AppendJoin(separator, tokens);

        return this;
    }

    public IMessageContentBuilder AddText(char separator, IEnumerable<string> tokens)
    {
        _text.AppendJoin(separator, tokens);

        return this;
    }

    public IMessageContentBuilder AddText(string token, int repeat)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(repeat, nameof(repeat));

        if (repeat is 0) return this;

        if (token.Length is 0) return this;

        if (repeat is 1) return AddText(token);

        for (var i = 0; i < repeat; i++)
        {
            _text.Append(token);
        }

        return this;
    }

    public IMessageContentBuilder AddText(char token, int repeat)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(repeat, nameof(repeat));

        if (repeat is 0) return this;

        if (repeat is 1) return AddText(new ReadOnlySpan<char>(ref token));

        _text.Append(token, repeat);

        return this;
    }

    public IMessageContentBuilder AddText(ReadOnlySpan<char> token, int repeat)
    {
        return AddText(token.ToString(), repeat);
    }

    public IMessageContentBuilder AddText(ReadOnlyMemory<char> token, int repeat)
    {
        return AddText(token.ToString(), repeat);
    }

    public IMessageContentBuilder AddText(char text)
    {
        _text.Append(text);

        return this;
    }

    public IMessageContentBuilder AddText(string text)
    {
        _text.Append(text);

        return this;
    }

    public IMessageContentBuilder AddText(ReadOnlySpan<char> text)
    {
        _text.Append(text);

        return this;
    }

    public IMessageContentBuilder AddText(ReadOnlyMemory<char> text)
    {
        _text.Append(text.Span);

        return this;
    }

    public IMessageContentBuilder AddStyle(IMessageTextStyle style)
    {
        _styles.Add(style);

        return this;
    }

    public MessageContent Build()
    {
        return new MessageContent(_text.ToString(), _styles.ToFrozenSequence());
    }
}
