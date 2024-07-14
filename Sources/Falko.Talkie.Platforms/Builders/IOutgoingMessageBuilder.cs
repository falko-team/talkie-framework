using Talkie.Models.Messages;

namespace Talkie.Builders;

public interface IOutgoingMessageBuilder
{
    IOutgoingMessageBuilder AddReply(IMessage reply);

    IOutgoingMessageBuilder AddText(string text);

    IOutgoingMessageBuilder AddTextLine(string text);

    IOutgoingMessageBuilder AddTextLine();

    IMessage Build();
}
