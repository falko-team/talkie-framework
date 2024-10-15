using Talkie.Models.Identifiers;

namespace Talkie.Models.Messages.Outgoing;

public interface IOutgoingMessageMutator : IMessageMutator<IOutgoingMessageMutator, IOutgoingMessage>
{
    IOutgoingMessageMutator MutateReply(Func<GlobalMessageIdentifier?, GlobalMessageIdentifier?> replyMutationFactory);
}
