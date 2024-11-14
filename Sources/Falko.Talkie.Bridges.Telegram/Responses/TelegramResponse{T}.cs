using Talkie.Bridges.Telegram.Models;

namespace Talkie.Bridges.Telegram.Responses;

public sealed class TelegramResponse<T>
(
    bool ok,
    T? result = default,
    int? errorCode = null,
    string? description = null,
    IReadOnlyDictionary<string, TextOrNumberValue>? parameters = null
)
{
    public readonly bool Ok = ok;

    public readonly T? Result = result;

    public readonly int? ErrorCode = errorCode;

    public readonly string? Description = description;

    public readonly IReadOnlyDictionary<string, TextOrNumberValue>? Parameters = parameters;
}
