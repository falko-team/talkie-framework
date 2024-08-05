namespace Talkie.Models.Messages;

public sealed class OutgoingMessageMutator : IOutgoingMessageMutator
{
    private readonly OutgoingMessage _message;

    private string? _text;

    private GlobalIdentifier? _reply;

    internal OutgoingMessageMutator(OutgoingMessage message)
    {
        _message = message;
        _text = message.Text;
        _reply = message.Reply;
    }

    public IOutgoingMessageMutator MutateText(Func<string?, string?> textMutationFactory)
    {
        _text = textMutationFactory(_text);

        return this;
    }

    public IOutgoingMessageMutator MutateReply(Func<GlobalIdentifier?, GlobalIdentifier?> replyMutationFactory)
    {
        _reply = replyMutationFactory(_reply);

        return this;
    }

    public IOutgoingMessage Mutate()
    {
        return new OutgoingMessage
        {
            Text = _text,
            Reply = _reply
        };
    }
}
