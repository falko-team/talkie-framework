using System.Text;
using Talkie.Sequences;

namespace Talkie.Models.Messages.Contents;

public sealed class MessageContentBuilder : IMessageContentBuilder
{
    private readonly StringBuilder _text = new();

    private readonly Sequence<IMessageTextStyle> _styles = new();

    public int TextLength => _text.Length;

    public int StylesCount => _styles.Count;

    public IMessageContentBuilder AddText(string text)
    {
        _text.Append(text);

        return this;
    }

    public IMessageContentBuilder AddTextStyle(IMessageTextStyle style)
    {
        _styles.Add(style);

        return this;
    }

    public MessageContent Build()
    {
        return new MessageContent(_text.ToString(), _styles.ToFrozenSequence());
    }
}
