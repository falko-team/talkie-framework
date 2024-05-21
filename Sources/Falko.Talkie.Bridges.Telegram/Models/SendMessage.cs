namespace Talkie.Bridges.Telegram.Models;

public sealed class SendMessage(
    long chatId,
    string text)
{
    public readonly long ChatId = chatId;

    public readonly string Text = text;
}
