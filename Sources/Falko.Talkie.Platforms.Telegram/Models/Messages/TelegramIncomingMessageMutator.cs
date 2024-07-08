namespace Talkie.Models.Messages;

public sealed class TelegramIncomingMessageMutator : IIncomingMessageMutator
{
    private readonly TelegramIncomingMessage _message;

    private string? _text;

    private IMessage? _reply;

    internal TelegramIncomingMessageMutator(TelegramIncomingMessage message)
    {
        _message = message;
        _text = message.Text;
        _reply = message.Reply;
    }

    public IIncomingMessageMutator MutateText(Func<string?, string?> textMutationFactory)
    {
        _text = textMutationFactory(_text);

        return this;
    }

    public IIncomingMessageMutator MutateReply(Func<IMessage?, IMessage?> replyMutationFactory)
    {
        _reply = replyMutationFactory(_reply);

        return this;
    }

    public TelegramIncomingMessage Mutate()
    {
        return _message with
        {
            Text = _text,
            Reply = _reply
        };
    }

    IIncomingMessage IIncomingMessageMutator.Mutate()
    {
        return Mutate();
    }
}
