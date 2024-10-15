namespace Talkie.Bridges.Telegram.Models;

public sealed class Response
(
    int? errorCode = null,
    string? description = null,
    IReadOnlyDictionary<string, TextOrNumber>? parameters = null
)
{
    public readonly int? ErrorCode = errorCode;

    public readonly string? Description = description;

    public readonly IReadOnlyDictionary<string, TextOrNumber>? Parameters = parameters;
}
