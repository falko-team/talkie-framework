namespace Talkie.Models.Messages;

public interface IIncomingMessageMutator
{
    IIncomingMessageMutator ContentMutation(Func<string?, string?> content);

    IIncomingMessage Mutate();
}
