namespace Talkie.Models.Messages;

public interface IIncomingMessage : Message.IWithPlatform, Message.IWithIdentifier, Message.IWithEntry, Message.IWithReply
{
    IIncomingMessage Mutate(Func<IIncomingMessageMutator, IIncomingMessageMutator> mutation);
}
