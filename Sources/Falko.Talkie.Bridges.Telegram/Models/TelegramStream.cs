namespace Talkie.Bridges.Telegram.Models;

public readonly record struct TelegramStream(int Identifier, Stream Stream, string? Name = null)
{
    public string ToAttach() => $"attach://{Identifier}";
}
