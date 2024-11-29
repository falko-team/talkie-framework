namespace Talkie.Sequences;

public static partial class SequenceExtensions
{
    public static SequenceAdapter<T> Sequencing<T>(this IEnumerable<T> sequence)
    {
        return new SequenceAdapter<T>(sequence);
    }

    public static bool Any<T>(this SequenceAdapter<T> sequence)
    {
        return sequence switch
        {
            { Sequence: FrozenSequence<T> frozenSequence } => frozenSequence.Any(),
            { Sequence: Sequence<T> addableSequence } => addableSequence.Any(),
            { Sequence: RemovableSequence<T> removableSequence } => removableSequence.Any(),
            _ => sequence.Sequence.Any()
        };
    }

    public static T? FirstOrDefault<T>(this SequenceAdapter<T> sequence)
    {
        return sequence switch
        {
            { Sequence: FrozenSequence<T> frozenSequence } => frozenSequence.FirstOrDefault(),
            { Sequence: Sequence<T> addableSequence } => addableSequence.FirstOrDefault(),
            { Sequence: RemovableSequence<T> removableSequence } => removableSequence.FirstOrDefault(),
            _ => sequence.Sequence.FirstOrDefault()
        };
    }

    public static T First<T>(this SequenceAdapter<T> sequence)
    {
        return sequence switch
        {
            { Sequence: FrozenSequence<T> frozenSequence } => frozenSequence.First(),
            { Sequence: Sequence<T> addableSequence } => addableSequence.First(),
            { Sequence: RemovableSequence<T> removableSequence } => removableSequence.First(),
            _ => sequence.Sequence.First()
        };
    }

    public static T? SingleOrDefault<T>(this SequenceAdapter<T> sequence)
    {
        return sequence switch
        {
            { Sequence: FrozenSequence<T> frozenSequence } => frozenSequence.SingleOrDefault(),
            { Sequence: Sequence<T> addableSequence } => addableSequence.SingleOrDefault(),
            { Sequence: RemovableSequence<T> removableSequence } => removableSequence.SingleOrDefault(),
            _ => sequence.Sequence.SingleOrDefault()
        };
    }

    public static T Single<T>(this SequenceAdapter<T> sequence)
    {
        return sequence switch
        {
            { Sequence: FrozenSequence<T> frozenSequence } => frozenSequence.Single(),
            { Sequence: Sequence<T> addableSequence } => addableSequence.Single(),
            { Sequence: RemovableSequence<T> removableSequence } => removableSequence.Single(),
            _ => sequence.Sequence.Single()
        };
    }

    public static T? LastOrDefault<T>(this SequenceAdapter<T> sequence)
    {
        return sequence switch
        {
            { Sequence: FrozenSequence<T> frozenSequence } => frozenSequence.LastOrDefault(),
            { Sequence: Sequence<T> addableSequence } => addableSequence.LastOrDefault(),
            { Sequence: RemovableSequence<T> removableSequence } => removableSequence.LastOrDefault(),
            _ => sequence.Sequence.LastOrDefault()
        };
    }

    public static T Last<T>(this SequenceAdapter<T> sequence)
    {
        return sequence switch
        {
            { Sequence: FrozenSequence<T> frozenSequence } => frozenSequence.Last(),
            { Sequence: Sequence<T> addableSequence } => addableSequence.Last(),
            { Sequence: RemovableSequence<T> removableSequence } => removableSequence.Last(),
            _ => sequence.Sequence.Last()
        };
    }

    public static bool Contains<T>(this SequenceAdapter<T> sequence, T value, IEqualityComparer<T> comparer)
    {
        return sequence switch
        {
            { Sequence: FrozenSequence<T> frozenSequence } => frozenSequence.Contains(value, comparer),
            { Sequence: Sequence<T> addableSequence } => addableSequence.Contains(value, comparer),
            { Sequence: RemovableSequence<T> removableSequence } => removableSequence.Contains(value, comparer),
            _ => sequence.Sequence.Contains(value, comparer)
        };
    }

    public static bool Contains<T>(this SequenceAdapter<T> sequence, T value)
    {
        return sequence switch
        {
            { Sequence: FrozenSequence<T> frozenSequence } => frozenSequence.Contains(value),
            { Sequence: Sequence<T> addableSequence } => addableSequence.Contains(value),
            { Sequence: RemovableSequence<T> removableSequence } => removableSequence.Contains(value),
            _ => sequence.Sequence.Contains(value)
        };
    }
}
