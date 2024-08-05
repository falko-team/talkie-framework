namespace Talkie.Models.Messages;

public interface IOutgoingMessageBuilder
{
    IOutgoingMessageBuilder SetReply(GlobalIdentifier reply);

    IOutgoingMessageBuilder AddText(string text);

    IOutgoingMessageBuilder AddTextLine(string text);

    IOutgoingMessageBuilder AddTextLine();

    IOutgoingMessage Build();
}
