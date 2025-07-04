namespace Falko.Talkie.Sequences;

public static partial class SequenceExtensions
{
    public static FrozenSequence<T> ToFrozenSequence<T>(this IEnumerable<T> enumerable)
    {
        return FrozenSequence.Copy(enumerable);
    }

    public static FrozenSequence<T> ToFrozenSequence<T>(this IReadOnlyCollection<T> values)
    {
        return FrozenSequence.Copy(values);
    }

    public static FrozenSequence<T> ToFrozenSequence<T>(this FrozenSequence<T> values)
    {
        ArgumentNullException.ThrowIfNull(values);

        return values;
    }
}
