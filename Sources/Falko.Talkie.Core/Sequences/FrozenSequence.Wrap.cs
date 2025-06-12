namespace Falko.Talkie.Sequences;

/// <summary>
/// Represents a thread-safe, immutable sequence of elements.
/// </summary>
public static partial class FrozenSequence
{
    /// <summary>
    /// Wraps the specified items into a <see cref="FrozenSequence{T}"/>.
    /// </summary>
    /// <param name="items">The items to wrap.</param>
    /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
    /// <returns>A new <see cref="FrozenSequence{T}"/> containing the specified items.</returns>
    public static FrozenSequence<T> Wrap<T>(params T[] items) => new(items, items.Length);

    /// <summary>
    /// Wraps the specified item into a <see cref="FrozenSequence{T}"/>.
    /// </summary>
    /// <param name="item">The item to wrap.</param>
    /// <typeparam name="T">The type of the element in the sequence.</typeparam>
    /// <returns>A new <see cref="FrozenSequence{T}"/> containing the specified item.</returns>
    public static FrozenSequence<T> Wrap<T>(T item) => new([item], 1);
}
