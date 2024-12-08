using System.Collections.Frozen;
using System.Net;
using System.Text;
using Talkie.Bridges.Telegram.Models;
using Talkie.Validation;

namespace Talkie.Bridges.Telegram.Clients;

public sealed partial class TelegramException
(
    ITelegramClient client,
    string methodName,
    HttpStatusCode? statusCode = null,
    string? description = null,
    IReadOnlyDictionary<string, TextOrNumberValue>? parameters = null,
    Exception? innerException = null
) : Exception(null, innerException)
{
    public ITelegramClient Client => Assert
        .ArgumentNullException.ThrowIfNull(client, nameof(client));

    public HttpStatusCode? StatusCode => statusCode;

    public string MethodName => Assert
        .ArgumentNullException.ThrowIfNullOrWhiteSpace(methodName, nameof(methodName));

    public string? Description => description;

    public IReadOnlyDictionary<string, TextOrNumberValue> Parameters { get; } = parameters ?? FrozenDictionary<string, TextOrNumberValue>.Empty;

    public override string HelpLink => $"https://core.telegram.org/bots/api#{MethodName}";

    public override string Message => GetFormattedMessage();

    private string GetFormattedMessage()
    {
        var strings = new StringBuilder();

        strings.Append($"{MethodName}()");

        if (StatusCode is not null)
        {
            strings.Append($", {nameof(StatusCode)}: {StatusCode}");
        }

        if (Description is not null)
        {
            strings.Append($", {nameof(Description)}: {Description}");
        }

        if (Parameters.Count is not 0)
        {
            strings.Append($", {nameof(Parameters)}: [");

            foreach (var (key, value) in Parameters)
            {
                strings.Append($"{key}={value}, ");
            }

            strings.Remove(strings.Length - 2, 2);
            strings.Append(']');
        }

        strings.Append($", {nameof(HelpLink)}: {HelpLink}");

        return strings.ToString();
    }
}
