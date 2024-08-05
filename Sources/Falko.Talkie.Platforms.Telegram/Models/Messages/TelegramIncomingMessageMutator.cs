namespace Talkie.Models.Messages;

public sealed class TelegramIncomingMessageMutator : IIncomingMessageMutator
{
    private readonly TelegramIncomingMessage _message;

    private string? _text;

    private TelegramIncomingMessage? _reply;

    internal TelegramIncomingMessageMutator(TelegramIncomingMessage message)
    {
        _message = message;
        _text = message.Text;
        _reply = message.Reply;
    }

    public TelegramIncomingMessageMutator MutateText(Func<string?, string?> textMutationFactory)
    {
        _text = textMutationFactory(_text);

        return this;
    }

    IIncomingMessageMutator IMessageMutator<IIncomingMessageMutator, IIncomingMessage>.MutateText(Func<string?, string?> textMutationFactory)
    {
        return MutateText(textMutationFactory);
    }

    public TelegramIncomingMessageMutator MutateReply(Func<TelegramIncomingMessage?, TelegramIncomingMessage?> replyMutationFactory)
    {
        _reply = replyMutationFactory(_reply);

        return this;
    }

    IIncomingMessageMutator IIncomingMessageMutator.MutateReply(Func<IIncomingMessage?, IIncomingMessage?> replyMutationFactory)
    {
        return MutateReply(replyMessage => (TelegramIncomingMessage?)replyMutationFactory(replyMessage));
    }

    public TelegramIncomingMessage Mutate()
    {
        return _message with
        {
            Text = _text,
            Reply = _reply
        };
    }

    IIncomingMessage IMessageMutator<IIncomingMessageMutator, IIncomingMessage>.Mutate()
    {
        return Mutate();
    }
}
