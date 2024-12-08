namespace Talkie.Sequences;

public static partial class SequenceExtensions
{
    public static FrozenSequence<T> ToFrozenSequence<T>(this IEnumerable<T> enumerable)
    {
        return FrozenSequence<T>.From(enumerable);
    }

    public static FrozenSequence<T> ToFrozenSequence<T>(this IReadOnlyCollection<T> values)
    {
        return FrozenSequence<T>.From(values);
    }

    public static FrozenSequence<T> ToFrozenSequence<T>(this FrozenSequence<T> values)
    {
        ArgumentNullException.ThrowIfNull(values);

        return values;
    }
}
