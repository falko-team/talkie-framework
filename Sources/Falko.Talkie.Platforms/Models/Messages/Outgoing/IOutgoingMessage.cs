using Talkie.Models.Identifiers;

namespace Talkie.Models.Messages.Outgoing;

public interface IOutgoingMessage : IMessage
{
    GlobalMessageIdentifier? Reply { get; }

    IOutgoingMessageMutator ToMutator();
}
