namespace Talkie.Models.Messages;

public interface IOutgoingMessageMutator : IMessageMutator<IOutgoingMessageMutator, IOutgoingMessage>
{
    IOutgoingMessageMutator MutateReply(Func<GlobalIdentifier?, GlobalIdentifier?> replyMutationFactory);
}
