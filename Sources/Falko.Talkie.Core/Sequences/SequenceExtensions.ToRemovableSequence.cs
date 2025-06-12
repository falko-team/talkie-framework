namespace Falko.Talkie.Sequences;

public static partial class SequenceExtensions
{
    public static RemovableSequence<T> ToRemovableSequence<T>(this IEnumerable<T> values) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(values);

        var sequence = new RemovableSequence<T>();

        sequence.AddRange(values);

        return sequence;
    }

    public static RemovableSequence<T> ToRemovableSequence<T>(this IReadOnlyCollection<T> values) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(values);

        if (values.Count is 0) return [];

        var sequence = new RemovableSequence<T>();

        sequence.AddRange(values);

        return sequence;
    }

    public static RemovableSequence<T> ToRemovableSequence<T>(this Sequence<T> values) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(values);

        if (values.Count is 0) return [];

        var sequence = new RemovableSequence<T>();

        sequence.AddRange(values);

        return sequence;
    }

    public static RemovableSequence<T> ToRemovableSequence<T>(this RemovableSequence<T> values) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(values);

        if (values.Count is 0) return [];

        var sequence = new RemovableSequence<T>();

        sequence.AddRange(values);

        return sequence;
    }

    public static RemovableSequence<T> ToRemovableSequence<T>(this FrozenSequence<T> values) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(values);

        if (values.Count is 0) return [];

        var sequence = new RemovableSequence<T>();

        sequence.AddRange(values);

        return sequence;
    }
}
