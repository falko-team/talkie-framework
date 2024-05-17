using Falko.Unibot.Models.Messages;

namespace Falko.Unibot.Builders;

public interface IOutgoingMessageBuilder
{
    IOutgoingMessageBuilder AddText(string text);

    IMessage Build();
}
