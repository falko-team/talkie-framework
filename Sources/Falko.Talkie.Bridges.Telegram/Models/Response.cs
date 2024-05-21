using System.Collections.Frozen;

namespace Talkie.Bridges.Telegram.Models;

public sealed class Response<T>(
    bool ok,
    T? result = null,
    int? errorCode = null,
    string? description = null,
    FrozenDictionary<string, string>? parameters = null) where T : class
{
    public readonly bool Ok = ok;

    public readonly T? Result = result;

    public readonly int? ErrorCode = errorCode;

    public readonly string? Description = description;

    public readonly FrozenDictionary<string, string>? Parameters = parameters;
}
