namespace Talkie.Models.Messages;

public interface IIncomingMessageMutator
{
    IIncomingMessageMutator MutateText(Func<string?, string?> textMutationFactory);

    IIncomingMessageMutator MutateReply(Func<IMessage?, IMessage?> replyMutationFactory);

    IIncomingMessage Mutate();
}
