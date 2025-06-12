namespace Falko.Talkie.Models.Messages.Incoming;

public interface IIncomingMessageMutator : IMessageMutator<IIncomingMessageMutator, IIncomingMessage>
{
    IIncomingMessageMutator MutateReply(Func<IIncomingMessage?, IIncomingMessage?> replyMutationFactory);
}
