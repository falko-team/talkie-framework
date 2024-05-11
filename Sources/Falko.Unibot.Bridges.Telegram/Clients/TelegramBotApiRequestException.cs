using System.Text;

namespace Falko.Unibot.Bridges.Telegram.Clients;

public sealed class TelegramBotApiRequestException : Exception
{
    public TelegramBotApiRequestException(string method,
        string? message = null,
        int? code = null,
        IDictionary<string, string>? parameters = null,
        Exception? inner = null) : base(message, inner)
    {
        Method = method;
        Code = code;
        Parameters = parameters;
    }

    public int? Code { get; }

    public IDictionary<string, string>? Parameters { get; }

    public string Method { get; }

    public override string HelpLink => $"https://core.telegram.org/bots/api#{Method}";

    public override string ToString()
    {
        var builder = new StringBuilder();

        builder.Append($"Method: {Method}");

        builder.Append($", Message: {Message}");

        builder.Append($", Code: {Code}");

        builder.Append($", Parameters: {(Parameters is not null
            ? string.Join(", ", Parameters)
            : Parameters)}");

        return builder.ToString();
    }
}
