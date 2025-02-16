namespace Talkie.Sequences;

public static partial class FrozenSequence
{
    public static FrozenSequence<T> Copy<T>(IEnumerable<T> enumerable)
    {
        ArgumentNullException.ThrowIfNull(enumerable);

        if (enumerable is FrozenSequence<T> frozenSequence) return frozenSequence;

        var items = enumerable.ToArray();
        var itemsLength = items.Length;

        return itemsLength is not 0
            ? new FrozenSequence<T>(items, itemsLength)
            : EmptyCache<T>.Instance;
    }

    public static FrozenSequence<T> Copy<T>(IReadOnlyCollection<T> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);

        if (collection.Count is 0) return EmptyCache<T>.Instance;

        if (collection is FrozenSequence<T> frozenSequence) return frozenSequence;

        var items = collection.ToArray();
        var itemsLength = items.Length;

        return itemsLength is not 0
            ? new FrozenSequence<T>(items, itemsLength)
            : EmptyCache<T>.Instance;
    }
}
