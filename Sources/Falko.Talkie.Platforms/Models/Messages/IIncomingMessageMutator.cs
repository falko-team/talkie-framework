namespace Talkie.Models.Messages;

public interface IIncomingMessageMutator : IMessageMutator<IIncomingMessageMutator, IIncomingMessage>
{
    IIncomingMessageMutator MutateReply(Func<IIncomingMessage?, IIncomingMessage?> replyMutationFactory);
}
