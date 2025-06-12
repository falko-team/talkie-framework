using System.Collections.Frozen;
using System.Text.Json;
using System.Text.Json.Serialization;
using Falko.Talkie.Bridges.Telegram.Models;
using Falko.Talkie.Sequences;

namespace Falko.Talkie.Bridges.Telegram.Serialization;

internal sealed class TextOrNumberValueDictionaryConverter : JsonConverter<IReadOnlyDictionary<string, TextOrNumberValue>>
{
    public override IReadOnlyDictionary<string, TextOrNumberValue> Read
    (
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        if (reader.TokenType is not JsonTokenType.StartObject)
        {
            throw new JsonException("Expected start object.");
        }

        var pairs = new Sequence<KeyValuePair<string, TextOrNumberValue>>();

        while (reader.Read())
        {
            if (reader.TokenType is JsonTokenType.EndObject)
            {
                return pairs.ToFrozenDictionary();
            }

            if (reader.TokenType is not JsonTokenType.PropertyName)
            {
                throw new JsonException("Expected property name.");
            }

            var key = reader.GetString() ?? throw new JsonException("Key is null.");

            if (reader.Read() is false)
            {
                throw new JsonException("Expected property value.");
            }

            var value = reader.TokenType switch
            {
                // We don't check for null here, because it will be checked in the constructor
                JsonTokenType.String => new TextOrNumberValue(reader.GetString()!),
                JsonTokenType.Number => new TextOrNumberValue(reader.GetInt64()),
                _ => throw new JsonException($"Unexpected value type for property '{key}', expected string or number.")
            };

            pairs.Add(new KeyValuePair<string, TextOrNumberValue>(key, value));
        }

        throw new JsonException("Expected end object.");
    }

    public override void Write
    (
        Utf8JsonWriter writer,
        IReadOnlyDictionary<string, TextOrNumberValue> dictionary,
        JsonSerializerOptions options
    )
    {
        writer.WriteStartObject();

        foreach (var pair in dictionary)
        {
            var value = pair.Value;

            if (value.TryGetNumber(out var number))
            {
                writer.WriteNumber(pair.Key, number);
            }
            else if (value.TryGetText(out var text))
            {
                writer.WriteString(pair.Key, text);
            }
            else
            {
                throw new JsonException($"Value for key '{pair.Key}' does not contain a number or text, or empty.");
            }
        }

        writer.WriteEndObject();
    }
}
