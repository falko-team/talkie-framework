namespace Talkie.Models.Messages;

public sealed class TelegramIncomingMessageMutator : IIncomingMessageMutator
{
    private readonly TelegramIncomingMessage _message;

    private string? _content;

    internal TelegramIncomingMessageMutator(TelegramIncomingMessage message)
    {
        _message = message;
        _content = message.Content;
    }

    public IIncomingMessageMutator ContentMutation(Func<string?, string?> content)
    {
        _content = content(_content);

        return this;
    }

    public TelegramIncomingMessage Mutate()
    {
        return _message with
        {
            Content = _content
        };
    }

    IIncomingMessage IIncomingMessageMutator.Mutate()
    {
        return Mutate();
    }
}
