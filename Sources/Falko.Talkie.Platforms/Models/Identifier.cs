using System.Diagnostics.CodeAnalysis;
using Falko.Talkie.Validations;

namespace Falko.Talkie.Models;

/// <summary>
/// Represents an identifier that contains not type strongly value and provides a <b>type-safe</b> way to work with it.
/// </summary>
public readonly struct Identifier
{
    /// <summary>
    /// Gets an empty <see cref="Identifier"/>.
    /// </summary>
    public static readonly Identifier Empty = new(null);

    private readonly object? _value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Identifier"/> struct.
    /// </summary>
    /// <exception cref="NotSupportedException">The constructor is not supported.</exception>
    [Obsolete("Use the empty field instead.")]
    public Identifier()
    {
        throw new NotSupportedException();
    }

    private Identifier(object? value) => _value = value;

    /// <summary>
    /// If the value of the identifier is <b>Null</b> returns <b>True</b>; otherwise, <b>False</b>.
    /// </summary>
    public bool IsValueEmpty => _value is null;

    /// <summary>
    /// Checks whether the value type of the identifier is the specified type.
    /// </summary>
    /// <typeparam name="T">The type to check.</typeparam>
    /// <returns>
    /// <b>True</b> if the value type of the identifier is the specified type; otherwise, <b>False</b>.
    /// </returns>
    public bool IsValueType<T>() where T : notnull
    {
        return _value is T;
    }

    /// <summary>
    /// Gets the value of the identifier if it is the specified type; otherwise, returns the default value.
    /// </summary>
    /// <typeparam name="T">The type of the value to get.</typeparam>
    /// <returns>
    /// <b>Null</b> or <b>Default</b> if the value type of the identifier is not the specified type;
    /// otherwise, the value of the identifier.
    /// </returns>
    public T? GetValueOrDefault<T>() where T : notnull
    {
        return _value is T typedValue
            ? typedValue
            : default!;
    }

    /// <summary>
    /// Tries to get the value of the identifier if it is the specified type and not <b>Null</b>.
    /// </summary>
    /// <param name="value">The value of the identifier if it is the specified type and not <b>Null</b>.</param>
    /// <typeparam name="T">The type of the value to get.</typeparam>
    /// <returns>
    /// <b>True</b> if the value type of the identifier is the specified type and not <b>Null</b>;
    /// otherwise, <b>False</b>.
    /// </returns>
    public bool TryGetValue<T>([MaybeNullWhen(false)] out T value) where T : notnull
    {
        if (_value is T typedValue)
        {
            value = typedValue;
            return true;
        }

        value = default!;
        return false;
    }

    public override string ToString()
    {
        const string emptyValueString = "empty";

        var valueString = _value?.ToString();

        var prettiedValueString = string.IsNullOrEmpty(valueString)
            ? emptyValueString
            : valueString;

        return $"{nameof(Identifier)}({prettiedValueString})";
    }

    public override bool Equals(object? other)
    {
        return other is Identifier identifier && Equals(identifier);
    }

    public bool Equals(Identifier other) => Equals(_value, other._value);

    public override int GetHashCode()
    {
        return _value != null
            ? _value.GetHashCode()
            : 0;
    }

    /// <summary>
    /// Creates a new <see cref="Identifier"/> from the specified value.
    /// </summary>
    /// <param name="value">The value to create the identifier from.</param>
    /// <typeparam name="T">The type of the value to create the identifier from.</typeparam>
    /// <returns>The created identifier.</returns>
    /// <exception cref="ArgumentNullException">The value is <b>Null</b>.</exception>
    public static Identifier FromValue<T>(T value) where T : notnull
    {
        value.ThrowIf().Null();

        return new Identifier(value);
    }

    public static bool operator ==(Identifier left, Identifier right) => left.Equals(right);

    public static bool operator !=(Identifier left, Identifier right) => left.Equals(right) is false;

    public static implicit operator Identifier(short value) => FromValue(value);

    public static implicit operator Identifier(int value) => FromValue(value);

    public static implicit operator Identifier(uint value) => FromValue(value);

    public static implicit operator Identifier(nint value) => FromValue(value);

    public static implicit operator Identifier(nuint value) => FromValue(value);

    public static implicit operator Identifier(long value) => FromValue(value);

    public static implicit operator Identifier(ulong value) => FromValue(value);

    public static implicit operator Identifier(Guid value) => FromValue(value);
}
