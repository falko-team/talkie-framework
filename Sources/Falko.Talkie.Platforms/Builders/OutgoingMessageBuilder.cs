using System.Text;
using Talkie.Models.Messages;

namespace Talkie.Builders;

public sealed class OutgoingMessageBuilder : IOutgoingMessageBuilder
{
    private readonly StringBuilder _content = new();

    private IMessage? _reply;

    public IOutgoingMessageBuilder AddReply(IMessage reply)
    {
        _reply = reply;

        return this;
    }

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
            Content = _content.ToString(),
            Reply = _reply
        };
    }
}
