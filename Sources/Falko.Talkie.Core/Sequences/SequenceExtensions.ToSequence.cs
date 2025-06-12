namespace Falko.Talkie.Sequences;

public static partial class SequenceExtensions
{
    public static Sequence<T> ToSequence<T>(this IEnumerable<T> values)
    {
        var sequence = new Sequence<T>();

        sequence.AddRange(values);

        return sequence;
    }

    public static Sequence<T> ToSequence<T>(this IReadOnlyCollection<T> values)
    {
        if (values.Count is 0) return [];

        var sequence = new Sequence<T>();

        sequence.AddRange(values);

        return sequence;
    }

    public static Sequence<T> ToSequence<T>(this Sequence<T> values)
    {
        if (values.Count is 0) return [];

        var sequence = new Sequence<T>();

        sequence.AddRange(values);

        return sequence;
    }

    public static Sequence<T> ToSequence<T>(this RemovableSequence<T> values)
    {
        if (values.Count is 0) return [];

        var sequence = new Sequence<T>();

        sequence.AddRange(values);

        return sequence;
    }

    public static Sequence<T> ToSequence<T>(this FrozenSequence<T> values)
    {
        if (values.Count is 0) return [];

        var sequence = new Sequence<T>();

        sequence.AddRange(values);

        return sequence;
    }
}
