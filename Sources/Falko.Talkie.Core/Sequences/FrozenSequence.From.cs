namespace Talkie.Sequences;

public partial class FrozenSequence<T>
{
    private FrozenSequence(T[] items, int itemsCount) => (_items, _itemsCount) = (items, itemsCount);

    public FrozenSequence(params T[] items) : this(items, items.Length) { }

    public static FrozenSequence<T> From(IEnumerable<T> enumerable)
    {
        ArgumentNullException.ThrowIfNull(enumerable);

        if (enumerable is FrozenSequence<T> frozenSequence) return frozenSequence;

        var items = enumerable.ToArray();
        var itemsLength = items.Length;

        return itemsLength is not 0
            ? new FrozenSequence<T>(items, itemsLength)
            : Empty;
    }

    public static FrozenSequence<T> From(IReadOnlyCollection<T> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);

        if (collection.Count is 0) return Empty;

        if (collection is FrozenSequence<T> frozenSequence) return frozenSequence;

        var items = collection.ToArray();
        var itemsLength = items.Length;

        return itemsLength is not 0
            ? new FrozenSequence<T>(items, itemsLength)
            : Empty;
    }
}
