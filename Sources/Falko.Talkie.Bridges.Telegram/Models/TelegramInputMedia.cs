using System.Text.Json.Serialization;

namespace Talkie.Bridges.Telegram.Models;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(TelegramInputMediaPhoto), "photo")]
public abstract class TelegramInputMedia
{
    public abstract string Type { get; }
}
