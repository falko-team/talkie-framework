using System.Text;

namespace Talkie.Bridges.Telegram.Clients;

public sealed class TelegramBotApiRequestException(
    string method,
    string? message = null,
    int? code = null,
    IReadOnlyDictionary<string, string>? parameters = null,
    Exception? inner = null) : Exception(message, inner)
{
    public int? Code { get; } = code;

    public IReadOnlyDictionary<string, string>? Parameters { get; } = parameters;

    public string Method { get; } = method;

    public override string HelpLink => $"https://core.telegram.org/bots/api#{Method}";

    public override string Message => GetFormattedMessage();

    private string GetFormattedMessage()
    {
        var strings = new StringBuilder();

        strings.Append($"{Method}: {base.Message}");

        if (Code is not null)
        {
            strings.Append($", {nameof(Code)}: {Code}");
        }

        if (Parameters is null || Parameters.Count <= 0)
        {
            return strings.ToString();
        }

        strings.Append($", {nameof(Parameters)}: [");

        foreach (var (key, value) in Parameters)
        {
            strings.Append($"{key}={value}, ");
        }

        strings.Remove(strings.Length - 2, 2);
        strings.Append(']');

        return strings.ToString();
    }
}
