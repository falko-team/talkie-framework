using Talkie.Bridges.Telegram.Models;

namespace Talkie.Bridges.Telegram.Responses;

public sealed class TelegramResponse
(
    int? errorCode = null,
    string? description = null,
    IReadOnlyDictionary<string, TextOrNumberValue>? parameters = null
)
{
    public int? ErrorCode => errorCode;

    public string? Description => description;

    public IReadOnlyDictionary<string, TextOrNumberValue>? Parameters => parameters;
}
