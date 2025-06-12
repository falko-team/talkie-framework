using Falko.Talkie.Bridges.Telegram.Models;

namespace Falko.Talkie.Bridges.Telegram.Responses;

public sealed class TelegramResponse<T>
(
    bool ok,
    T? result = default,
    int? errorCode = null,
    string? description = null,
    IReadOnlyDictionary<string, TextOrNumberValue>? parameters = null
)
{
    public bool Ok => ok;

    public T? Result => result;

    public int? ErrorCode => errorCode;

    public string? Description => description;

    public IReadOnlyDictionary<string, TextOrNumberValue>? Parameters => parameters;
}
