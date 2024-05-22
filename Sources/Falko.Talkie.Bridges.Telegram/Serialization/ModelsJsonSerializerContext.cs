using System.Text.Json.Serialization;
using Talkie.Bridges.Telegram.Models;

namespace Talkie.Bridges.Telegram.Serialization;

[JsonSourceGenerationOptions(WriteIndented = false,
    GenerationMode = JsonSourceGenerationMode.Metadata,
    UseStringEnumConverter = true,
    IgnoreReadOnlyFields = false,
    IgnoreReadOnlyProperties = false,
    IncludeFields = true,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
    PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower,
    DictionaryKeyPolicy = JsonKnownNamingPolicy.SnakeCaseLower,
    Converters = [typeof(DateTimeUnixConverter)])]
[JsonSerializable(typeof(Response<Update[]>))]
[JsonSerializable(typeof(Response<User>))]
[JsonSerializable(typeof(Response<Message>))]
[JsonSerializable(typeof(GetUpdates))]
[JsonSerializable(typeof(SendMessage))]
internal sealed partial class ModelsJsonSerializerContext : JsonSerializerContext;
