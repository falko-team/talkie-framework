namespace Talkie.Bridges.Telegram.Models;

public sealed class SendMessage(
    long chatId,
    string text,
    ReplyParameters? replyParameters = null)
{
    public readonly long ChatId = chatId;

    public readonly string Text = text;

    public readonly ReplyParameters? ReplyParameters = replyParameters;
}
