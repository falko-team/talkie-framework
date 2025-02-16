namespace Talkie.Sequences;

public static partial class FrozenSequence
{
    /// <summary>
    /// Copy the given <paramref name="enumerable"/> to a new <see cref="FrozenSequence{T}"/>.
    /// </summary>
    /// <param name="enumerable">The enumerable to copy.</param>
    /// <typeparam name="T">The type of the elements in the enumerable.</typeparam>
    /// <returns>A new <see cref="FrozenSequence{T}"/> containing the elements of the given <paramref name="enumerable"/>.</returns>
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

    /// <summary>
    /// Copy the given <paramref name="collection"/> to a new <see cref="FrozenSequence{T}"/>.
    /// </summary>
    /// <param name="collection">The collection to copy.</param>
    /// <typeparam name="T">The type of the elements in the collection.</typeparam>
    /// <returns>A new <see cref="FrozenSequence{T}"/> containing the elements of the given <paramref name="collection"/>.</returns>
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
