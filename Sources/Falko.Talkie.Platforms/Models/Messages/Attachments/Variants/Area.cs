namespace Talkie.Models.Messages.Attachments.Variants;

public readonly partial struct Area : IComparable<Area>, IComparable, IEquatable<Area>
{
    public Area(long width, long height)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(width, nameof(width));
        ArgumentOutOfRangeException.ThrowIfNegative(height, nameof(height));

        Width = width;
        Height = height;
    }

    public readonly long Width;

    public readonly long Height;

    public int CompareTo(Area other) => Width > Height
        ? Width.CompareTo(other.Width)
        : Height.CompareTo(other.Height);

    public int CompareTo(object? obj) => obj is Area other
        ? CompareTo(other)
        : 1;

    public bool Equals(Area other) => Width == other.Width && Height == other.Height;

    public override bool Equals(object? obj) => obj is Area other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(Width, Height);

    public static bool operator ==(Area target, Area other) => target.Equals(other);

    public static bool operator !=(Area target, Area other) => target.Equals(other) is false;

    public override string ToString() => $"{{Width={Width}, Height={Height}}}";
}
