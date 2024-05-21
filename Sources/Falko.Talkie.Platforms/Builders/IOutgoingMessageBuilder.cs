using Talkie.Models.Messages;

namespace Talkie.Builders;

public interface IOutgoingMessageBuilder
{
    IOutgoingMessageBuilder AddText(string text);

    IMessage Build();
}
