using System.Text.Json.Serialization;
using Talkie.Bridges.Telegram.Models;
using Talkie.Bridges.Telegram.Requests;
using Talkie.Bridges.Telegram.Responses;

namespace Talkie.Bridges.Telegram.Serialization;

[JsonSourceGenerationOptions(WriteIndented = false,
    GenerationMode = JsonSourceGenerationMode.Metadata,
    UseStringEnumConverter = true,
    IgnoreReadOnlyFields = false,
    IgnoreReadOnlyProperties = false,
    IncludeFields = true,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower,
    DictionaryKeyPolicy = JsonKnownNamingPolicy.SnakeCaseLower,
    Converters = [typeof(DateTimeUnixConverter), typeof(ReadOnlyStringsToTextOrNumberDictionaryConverter)])]
[JsonSerializable(typeof(TelegramResponse<TelegramUpdate[]>))]
[JsonSerializable(typeof(TelegramResponse<TelegramUser>))]
[JsonSerializable(typeof(TelegramResponse<TelegramMessage>))]
[JsonSerializable(typeof(TelegramResponse<bool>))]
[JsonSerializable(typeof(TelegramResponse<TelegramFile>))]
[JsonSerializable(typeof(TelegramGetFileRequest))]
[JsonSerializable(typeof(TelegramGetUpdatesRequest))]
[JsonSerializable(typeof(TelegramSendMessageRequest))]
[JsonSerializable(typeof(TelegramDeleteMessageRequest))]
[JsonSerializable(typeof(TelegramEditMessageTextRequest))]
internal sealed partial class ModelsJsonSerializerContext : JsonSerializerContext;
