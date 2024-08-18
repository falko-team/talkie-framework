using System.Text.Json.Serialization;

namespace Talkie.Bridges.Telegram.Models;

public sealed class Response<T>(
    bool ok,
    T? result = null,
    int? errorCode = null,
    string? description = null,
    IReadOnlyDictionary<string, TextOrNumber>? parameters = null) where T : class
{
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public readonly bool Ok = ok;

    public readonly T? Result = result;

    public readonly int? ErrorCode = errorCode;

    public readonly string? Description = description;

    public readonly IReadOnlyDictionary<string, TextOrNumber>? Parameters = parameters;
}
