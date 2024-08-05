namespace Talkie.Models.Messages;

public interface IOutgoingMessage : IMessage
{
    GlobalIdentifier? Reply { get; }

    IOutgoingMessageMutator ToMutator();
}
