using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Talkie.Bridges.Telegram.Models;

public readonly struct TextOrNumberValue
{
    private const string EmptyString = "empty";

    private readonly string? _textValue;

    private readonly long _numberValue;

    private readonly bool _hasValue;

    public TextOrNumberValue(string textValue)
    {
        ArgumentNullException.ThrowIfNull(textValue);

        _textValue = textValue;
        _hasValue = true;
    }

    public TextOrNumberValue(long numberValue)
    {
        _numberValue = numberValue;
        _hasValue = true;
    }

    public static TextOrNumberValue Empty => default;

    public bool IsEmpty => _hasValue is false;

    [MemberNotNullWhen(true, nameof(_textValue))]
    public bool ContainsText() => _textValue is not null;

    [MemberNotNullWhen(false, nameof(_textValue))]
    public bool ContainsNumber() => _hasValue && _textValue is null;

    public string GetText() => ContainsText() ? _textValue : throw new InvalidOperationException("Text is not set.");

    public long GetNumber() => ContainsNumber() ? _numberValue : throw new InvalidOperationException("Number is not set.");

    public bool TryGetText([MaybeNullWhen(false)] out string text)
    {
        text = _textValue;
        return ContainsText();
    }

    public bool TryGetNumber(out long number)
    {
        number = _numberValue;
        return ContainsNumber();
    }

    public override string ToString()
    {
        if (TryGetText(out var text))
        {
            return $"\"{text}\"";
        }

        if (TryGetNumber(out var number))
        {
            return number.ToString(CultureInfo.InvariantCulture);
        }

        return EmptyString;
    }
}
