using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Talkie.Validations;

namespace Talkie.Bridges.Telegram.Models;

public readonly struct TextOrNumber
{
    public static readonly TextOrNumber Empty = default;

    private readonly string? _text;

    private readonly long _number;

    private readonly bool _containsTextOrNumber;

    public TextOrNumber(string text)
    {
        text.ThrowIf().Null();

        _text = text;
        _containsTextOrNumber = true;
    }

    public TextOrNumber(long number)
    {
        _number = number;
        _containsTextOrNumber = true;
    }

    public bool IsEmpty => _containsTextOrNumber is false;

    [MemberNotNullWhen(true, nameof(_text))]
    public bool ContainsText() => _text is not null;

    [MemberNotNullWhen(false, nameof(_text))]
    public bool ContainsNumber() => _containsTextOrNumber && _text is null;

    public string GetText() => ContainsText() ? _text : throw new InvalidOperationException("Text is not set.");

    public long GetNumber() => ContainsNumber() ? _number : throw new InvalidOperationException("Number is not set.");

    public bool TryGetText([MaybeNullWhen(false)] out string text)
    {
        text = _text;
        return ContainsText();
    }

    public bool TryGetNumber(out long number)
    {
        number = _number;
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

        return nameof(Empty);
    }
}
