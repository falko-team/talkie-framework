namespace Talkie.Bridges.Telegram.Models;

public sealed class SendMessage(
    long chatId,
    string text,
    bool? disableNotification = null,
    ReplyParameters? replyParameters = null)
{
    public readonly long ChatId = chatId;

    public readonly string Text = text;

    public readonly bool? DisableNotification = disableNotification;

    public readonly ReplyParameters? ReplyParameters = replyParameters;
}
