namespace Talkie.Bridges.Telegram.Requests;

public abstract class TelegramRequest<T> : ITelegramRequest<T> where T : notnull
{
    private TelegramRequest() { }
}
