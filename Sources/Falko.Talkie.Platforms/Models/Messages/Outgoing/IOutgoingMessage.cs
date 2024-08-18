namespace Talkie.Models.Messages.Outgoing;

public interface IOutgoingMessage : IMessage
{
    GlobalIdentifier? Reply { get; }

    IOutgoingMessageMutator ToMutator();
}
