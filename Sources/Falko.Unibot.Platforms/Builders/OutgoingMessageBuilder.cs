using System.Text;
using Falko.Unibot.Models.Messages;

namespace Falko.Unibot.Builders;

public sealed class OutgoingMessageBuilder : IOutgoingMessageBuilder
{
    private readonly StringBuilder _content = new();

    public IOutgoingMessageBuilder AddText(string text)
    {
        _content.Append(text);

        return this;
    }

    public IMessage Build()
    {
        if (_content.Length == 0) return new OutgoingMessage();

        return new OutgoingMessage
        {
            Content = _content.ToString()
        };
    }
}
