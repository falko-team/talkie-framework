namespace Talkie.Models.Messages.Outgoing;

public interface IOutgoingMessageMutator : IMessageMutator<IOutgoingMessageMutator, IOutgoingMessage>
{
    IOutgoingMessageMutator MutateReply(Func<GlobalIdentifier?, GlobalIdentifier?> replyMutationFactory);
}
