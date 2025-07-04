using System.Runtime.Serialization;

namespace Falko.Talkie.Bridges.Telegram.Models;

public readonly record struct TelegramStream(int Identifier, [property: IgnoreDataMember] Stream Stream, string? Name = null)
{
    public string ToAttach() => $"attach://{Identifier}";
}
