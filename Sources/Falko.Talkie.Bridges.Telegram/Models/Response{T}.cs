namespace Talkie.Bridges.Telegram.Models;

public sealed class Response<T>
(
    bool ok,
    T? result = default,
    int? errorCode = null,
    string? description = null,
    IReadOnlyDictionary<string, TextOrNumber>? parameters = null
)
{
    public readonly bool Ok = ok;

    public readonly T? Result = result;

    public readonly int? ErrorCode = errorCode;

    public readonly string? Description = description;

    public readonly IReadOnlyDictionary<string, TextOrNumber>? Parameters = parameters;
}
