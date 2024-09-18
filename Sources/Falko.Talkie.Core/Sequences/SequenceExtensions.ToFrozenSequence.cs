using System.Runtime.CompilerServices;

namespace Talkie.Sequences;

public static partial class SequenceExtensions
{
    public static FrozenSequence<T> ToFrozenSequence<T>(this IEnumerable<T> values)
    {
        ArgumentNullException.ThrowIfNull(values);

        if (values is FrozenSequence<T> sequence) return sequence;

        return new FrozenSequence<T>(values);
    }

    public static FrozenSequence<T> ToFrozenSequence<T>(this IReadOnlyCollection<T> values)
    {
        ArgumentNullException.ThrowIfNull(values);

        if (values.Count is 0) return FrozenSequence<T>.Empty;

        if (values is FrozenSequence<T> sequence) return sequence;

        return new FrozenSequence<T>(values);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static FrozenSequence<T> ToFrozenSequence<T>(this FrozenSequence<T> values)
    {
        ArgumentNullException.ThrowIfNull(values);

        return values;
    }
}
