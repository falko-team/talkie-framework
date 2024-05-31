namespace Talkie.Models.Messages;

public interface IIncomingMessageMutator
{
    IIncomingMessageMutator MutateContent(Func<string?, string?> content);

    IIncomingMessage Mutate();
}
