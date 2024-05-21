using Falko.Talkie.Models.Messages;

namespace Falko.Talkie.Builders;

public interface IOutgoingMessageBuilder
{
    IOutgoingMessageBuilder AddText(string text);

    IMessage Build();
}
