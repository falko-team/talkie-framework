using System.Text.Json.Serialization;
using Falko.Talkie.Bridges.Telegram.Models;
using Falko.Talkie.Bridges.Telegram.Requests;
using Falko.Talkie.Bridges.Telegram.Responses;

namespace Falko.Talkie.Bridges.Telegram.Serialization;

[JsonSourceGenerationOptions(
    WriteIndented = false,
    GenerationMode = JsonSourceGenerationMode.Metadata,
    UseStringEnumConverter = true,
    IgnoreReadOnlyFields = false,
    IgnoreReadOnlyProperties = false,
    IncludeFields = true,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower,
    DictionaryKeyPolicy = JsonKnownNamingPolicy.SnakeCaseLower,
    Converters = [typeof(DateTimeUnixConverter), typeof(TextOrNumberValueDictionaryConverter)]
)]
[JsonSerializable(typeof(TelegramResponse<IReadOnlyList<TelegramUpdate>>))]
[JsonSerializable(typeof(TelegramResponse<TelegramUser>))]
[JsonSerializable(typeof(TelegramResponse<TelegramMessage>))]
[JsonSerializable(typeof(TelegramResponse<IReadOnlyList<TelegramMessage>>))]
[JsonSerializable(typeof(TelegramResponse<bool>))]
[JsonSerializable(typeof(TelegramResponse<TelegramFile>))]
[JsonSerializable(typeof(TelegramGetFileRequest))]
[JsonSerializable(typeof(TelegramGetUpdatesRequest))]
[JsonSerializable(typeof(TelegramSendMessageRequest))]
[JsonSerializable(typeof(TelegramDeleteMessageRequest))]
[JsonSerializable(typeof(TelegramEditMessageTextRequest))]
[JsonSerializable(typeof(TelegramSendPhotoRequest))]
[JsonSerializable(typeof(TelegramSendMediaGroupRequest))]
[JsonSerializable(typeof(TelegramSendStickerRequest))]
[JsonSerializable(typeof(TelegramSendAudioRequest))]
internal sealed partial class ModelsJsonSerializerContext : JsonSerializerContext;
