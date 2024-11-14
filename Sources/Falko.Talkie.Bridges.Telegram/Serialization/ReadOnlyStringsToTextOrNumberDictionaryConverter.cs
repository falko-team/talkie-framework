using System.Collections.Frozen;
using System.Text.Json;
using System.Text.Json.Serialization;
using Talkie.Bridges.Telegram.Models;

namespace Talkie.Bridges.Telegram.Serialization;

internal sealed class ReadOnlyStringsToTextOrNumberDictionaryConverter
    : JsonConverter<IReadOnlyDictionary<string, TextOrNumberValue>>
{
    public override IReadOnlyDictionary<string, TextOrNumberValue> Read(ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        if (reader.TokenType is not JsonTokenType.StartObject)
        {
            throw new JsonException("Expected start object");
        }

        var dictionary = new Dictionary<string, TextOrNumberValue>();

        while (reader.Read())
        {
            if (reader.TokenType is JsonTokenType.EndObject)
            {
                return dictionary.ToFrozenDictionary();
            }

            if (reader.TokenType is not JsonTokenType.PropertyName)
            {
                throw new JsonException("Expected property name");
            }

            var key = reader.GetString() ?? throw new JsonException("Key is null");

            if (reader.Read() is false)
            {
                throw new JsonException("Expected string value");
            }

            if (reader.TokenType is JsonTokenType.String)
            {
                var value = new TextOrNumberValue(reader.GetString() ?? throw new JsonException("Value is null"));

                dictionary.Add(key, value);
            }
            else if (reader.TokenType is JsonTokenType.Number)
            {
                var value = new TextOrNumberValue(reader.GetInt64());

                dictionary.Add(key, value);
            }
        }

        throw new JsonException("Expected end object");
    }

    public override void Write(Utf8JsonWriter writer,
        IReadOnlyDictionary<string, TextOrNumberValue> dictionary,
        JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (var (key, value) in dictionary)
        {
            if (value.TryGetNumber(out var number))
            {
                writer.WriteNumber(key, number);
            }
            else if (value.TryGetText(out var text))
            {
                writer.WriteString(key, text);
            }

            throw new JsonException("Value is empty");
        }

        writer.WriteEndObject();
    }
}
