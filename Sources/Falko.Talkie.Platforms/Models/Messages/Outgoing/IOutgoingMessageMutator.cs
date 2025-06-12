using Falko.Talkie.Models.Identifiers;

namespace Falko.Talkie.Models.Messages.Outgoing;

public interface IOutgoingMessageMutator : IMessageMutator<IOutgoingMessageMutator, IOutgoingMessage>
{
    IOutgoingMessageMutator MutateReply(Func<GlobalMessageIdentifier?, GlobalMessageIdentifier?> replyMutationFactory);
}
