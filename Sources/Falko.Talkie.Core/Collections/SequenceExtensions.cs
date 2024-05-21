namespace Falko.Talkie.Collections;

public static class SequenceExtensions
{
    public static Sequence<T> ToSequence<T>(this IEnumerable<T> enumerable) where T : notnull
    {
        if (enumerable is Sequence<T> sequence) return sequence;

        sequence = new Sequence<T>();

        sequence.AddRange(enumerable);

        return sequence;
    }

    public static RemovableSequence<T> ToRemovableSequence<T>(this IEnumerable<T> enumerable) where T : notnull
    {
        if (enumerable is RemovableSequence<T> sequence) return sequence;

        sequence = new RemovableSequence<T>();

        sequence.AddRange(enumerable);

        return sequence;
    }

    public static FrozenSequence<T> ToFrozenSequence<T>(this IEnumerable<T> enumerable) where T : notnull
    {
        return new FrozenSequence<T>(enumerable);
    }

    public static void AddRange<T>(this ISequence<T> sequence, IEnumerable<T> enumerable) where T : notnull
    {
        foreach (var value in enumerable)
        {
            sequence.Add(value);
        }
    }
}
