namespace Talkie.Sequences;

public static partial class SequenceExtensions
{
    public static SequencingAdapter<T> Sequencing<T>(this IEnumerable<T> sequence)
    {
        return new SequencingAdapter<T>(sequence);
    }

    public static bool Any<T>(this SequencingAdapter<T> adapter)
    {
        return adapter switch
        {
            { Enumerable: FrozenSequence<T> frozenSequence } => frozenSequence.Any(),
            { Enumerable: Sequence<T> addableSequence } => addableSequence.Any(),
            { Enumerable: RemovableSequence<T> removableSequence } => removableSequence.Any(),
            _ => adapter.Enumerable.Any()
        };
    }

    public static T? FirstOrDefault<T>(this SequencingAdapter<T> adapter)
    {
        return adapter switch
        {
            { Enumerable: FrozenSequence<T> frozenSequence } => frozenSequence.FirstOrDefault(),
            { Enumerable: Sequence<T> addableSequence } => addableSequence.FirstOrDefault(),
            { Enumerable: RemovableSequence<T> removableSequence } => removableSequence.FirstOrDefault(),
            _ => adapter.Enumerable.FirstOrDefault()
        };
    }

    public static T First<T>(this SequencingAdapter<T> adapter)
    {
        return adapter switch
        {
            { Enumerable: FrozenSequence<T> frozenSequence } => frozenSequence.First(),
            { Enumerable: Sequence<T> addableSequence } => addableSequence.First(),
            { Enumerable: RemovableSequence<T> removableSequence } => removableSequence.First(),
            _ => adapter.Enumerable.First()
        };
    }

    public static T? SingleOrDefault<T>(this SequencingAdapter<T> adapter)
    {
        return adapter switch
        {
            { Enumerable: FrozenSequence<T> frozenSequence } => frozenSequence.SingleOrDefault(),
            { Enumerable: Sequence<T> addableSequence } => addableSequence.SingleOrDefault(),
            { Enumerable: RemovableSequence<T> removableSequence } => removableSequence.SingleOrDefault(),
            _ => adapter.Enumerable.SingleOrDefault()
        };
    }

    public static T Single<T>(this SequencingAdapter<T> adapter)
    {
        return adapter switch
        {
            { Enumerable: FrozenSequence<T> frozenSequence } => frozenSequence.Single(),
            { Enumerable: Sequence<T> addableSequence } => addableSequence.Single(),
            { Enumerable: RemovableSequence<T> removableSequence } => removableSequence.Single(),
            _ => adapter.Enumerable.Single()
        };
    }

    public static T? LastOrDefault<T>(this SequencingAdapter<T> adapter)
    {
        return adapter switch
        {
            { Enumerable: FrozenSequence<T> frozenSequence } => frozenSequence.LastOrDefault(),
            { Enumerable: Sequence<T> addableSequence } => addableSequence.LastOrDefault(),
            { Enumerable: RemovableSequence<T> removableSequence } => removableSequence.LastOrDefault(),
            _ => adapter.Enumerable.LastOrDefault()
        };
    }

    public static T Last<T>(this SequencingAdapter<T> adapter)
    {
        return adapter switch
        {
            { Enumerable: FrozenSequence<T> frozenSequence } => frozenSequence.Last(),
            { Enumerable: Sequence<T> addableSequence } => addableSequence.Last(),
            { Enumerable: RemovableSequence<T> removableSequence } => removableSequence.Last(),
            _ => adapter.Enumerable.Last()
        };
    }

    public static bool Contains<T>(this SequencingAdapter<T> adapter, T value, IEqualityComparer<T> comparer)
    {
        return adapter switch
        {
            { Enumerable: FrozenSequence<T> frozenSequence } => frozenSequence.Contains(value, comparer),
            { Enumerable: Sequence<T> addableSequence } => addableSequence.Contains(value, comparer),
            { Enumerable: RemovableSequence<T> removableSequence } => removableSequence.Contains(value, comparer),
            _ => adapter.Enumerable.Contains(value, comparer)
        };
    }

    public static bool Contains<T>(this SequencingAdapter<T> adapter, T value)
    {
        return adapter switch
        {
            { Enumerable: FrozenSequence<T> frozenSequence } => frozenSequence.Contains(value),
            { Enumerable: Sequence<T> addableSequence } => addableSequence.Contains(value),
            { Enumerable: RemovableSequence<T> removableSequence } => removableSequence.Contains(value),
            _ => adapter.Enumerable.Contains(value)
        };
    }

    public static void ForEach<T>(this SequencingAdapter<T> adapter, Action<T> action)
    {
        switch (adapter)
        {
            case { Enumerable: FrozenSequence<T> frozenSequence }:
                foreach (var item in frozenSequence) action(item);
                break;
            case { Enumerable: Sequence<T> addableSequence }:
                foreach (var item in addableSequence) action(item);
                break;
            case { Enumerable: RemovableSequence<T> removableSequence }:
                foreach (var item in removableSequence) action(item);
                break;
            default:
                foreach (var item in adapter.Enumerable) action(item);
                break;
        }
    }
}
