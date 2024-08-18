using System.Text;

namespace Talkie.Models.Messages.Outgoing;

public sealed class OutgoingMessageBuilder : IOutgoingMessageBuilder
{
    private readonly StringBuilder _content = new();

    private GlobalIdentifier? _reply;

    public IOutgoingMessageBuilder SetReply(GlobalIdentifier reply)
    {
        _reply = reply;

        return this;
    }

    public IOutgoingMessageBuilder AddText(string text)
    {
        _content.Append(text);

        return this;
    }

    public IOutgoingMessageBuilder AddTextLine(string text)
    {
        _content.AppendLine(text);

        return this;
    }

    public IOutgoingMessageBuilder AddTextLine()
    {
        _content.AppendLine();

        return this;
    }

    public IOutgoingMessage Build()
    {
        if (_content.Length is 0) return new OutgoingMessage();

        return new OutgoingMessage
        {
            Text = _content.ToString(),
            Reply = _reply
        };
    }
}
