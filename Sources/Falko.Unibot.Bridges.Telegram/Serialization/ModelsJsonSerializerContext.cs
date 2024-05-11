using System.Text.Json.Serialization;
using Falko.Unibot.Bridges.Telegram.Models;

namespace Falko.Unibot.Bridges.Telegram.Serialization;

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
