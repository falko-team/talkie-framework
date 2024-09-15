using System.Runtime.CompilerServices;

namespace Talkie.Models.Messages.Contents;

public readonly struct MessageTextRange
{
    public static readonly MessageTextRange Empty = default;

    public readonly int Offset;

    public readonly int Length;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MessageTextRange(int offset, int length)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(offset, nameof(offset));
        ArgumentOutOfRangeException.ThrowIfNegative(length, nameof(length));

        Offset = offset;
        Length = length;
    }

    public bool IsEmpty => Length == 0;

    public bool Contains(MessageTextRange range) => Offset <= range.Offset && Offset + Length >= range.Offset + range.Length;

    public override bool Equals(object? obj) => obj is MessageTextRange range && Equals(range);

    public bool Equals(MessageTextRange other) => Offset == other.Offset && Length == other.Length;

    public static bool operator ==(MessageTextRange left, MessageTextRange right) => left.Equals(right);

    public static bool operator !=(MessageTextRange left, MessageTextRange right) => !left.Equals(right);

    public override int GetHashCode() => HashCode.Combine(Offset, Length);

    public static implicit operator MessageTextRange((int Offset, int Length) tuple) => new(tuple.Offset, tuple.Length);

    public void Deconstruct(out int offset, out int length)
    {
        offset = Offset;
        length = Length;
    }

    public override string ToString() => $"({Offset}, {Length})";
}
