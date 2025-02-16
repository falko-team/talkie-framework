using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Talkie.Sequences;

public partial class FrozenSequence<T>
{
    /// <summary>
    /// Determines whether any element of a sequence exists.
    /// </summary>
    /// <returns><see langword="true"/> if the source sequence contains any elements; otherwise, <see langword="false"/>.</returns>
    public bool Any()
    {
        return _itemsCount > 0;
    }

    /// <summary>
    /// Determines whether any element of a sequence exists.
    /// </summary>
    /// <returns><see langword="true"/> if the source sequence contains any elements; otherwise, <see langword="false"/>.</returns>
    public T? FirstOrDefault()
    {
        return _itemsCount is 0
            ? default
            : Unsafe.Add(ref MemoryMarshal.GetArrayDataReference(_items), 0);
    }

    /// <summary>
    /// Returns the first element of a sequence.
    /// </summary>
    /// <returns>The first element in the specified sequence.</returns>
    /// <exception cref="InvalidOperationException">The source sequence is empty.</exception>
    public T First()
    {
        return _itemsCount is not 0
            ? _items[0]
            : throw new InvalidOperationException("The source sequence is empty.");
    }

    /// <summary>
    /// Returns the only element of a sequence, or a default value if the sequence is empty.
    /// </summary>
    /// <returns>The single element of the input sequence, or <see langword="null"/> if the sequence contains no elements.</returns>
    public T? SingleOrDefault()
    {
        return _itemsCount is 1
            ? _items[0]
            : default;
    }

    /// <summary>
    /// Returns the only element of a sequence, and throws an exception if there is not exactly one element in the sequence.
    /// </summary>
    /// <returns>The single element of the input sequence.</returns>
    /// <exception cref="InvalidOperationException">The source sequence contains more than one element.</exception>
    public T Single()
    {
        return _itemsCount is 1
            ? Unsafe.Add(ref MemoryMarshal.GetArrayDataReference(_items), 0)
            : throw new InvalidOperationException("The source sequence contains more than one element.");
    }

    /// <summary>
    /// Returns the last element of a sequence.
    /// </summary>
    /// <returns>The last element in the specified sequence.</returns>
    /// <exception cref="InvalidOperationException">The source sequence is empty.</exception>
    public T Last()
    {
        return _itemsCount is not 0
            ? _items[_itemsCount - 1]
            : throw new InvalidOperationException("The source sequence is empty.");
    }

    /// <summary>
    /// Returns the last element of a sequence, or a default value if the sequence is empty.
    /// </summary>
    /// <returns>The last element in the specified sequence.</returns>
    public T? LastOrDefault()
    {
        return _itemsCount is 1
            ? _items[0]
            : default;
    }

    /// <summary>
    /// Determines whether any element of a sequence exists.
    /// </summary>
    /// <param name="value">The value to locate in the sequence.</param>
    /// <returns><see langword="true"/> if the source sequence contains the specified value; otherwise, <see langword="false"/>.</returns>
    public bool Contains(T value)
    {
        return Contains(value, EqualityComparer<T>.Default);
    }

    /// <summary>
    /// Determines whether any element of a sequence exists.
    /// </summary>
    /// <param name="value">The value to locate in the sequence.</param>
    /// <param name="comparer">The equality comparer to use in the comparison.</param>
    /// <returns><see langword="true"/> if the source sequence contains the specified value; otherwise, <see langword="false"/>.</returns>
    public bool Contains(T value, IEqualityComparer<T> comparer)
    {
        foreach (var item in this)
        {
            if (comparer.Equals(item, value)) return true;
        }

        return false;
    }

    /// <summary>
    /// Wraps the <see cref="FrozenSequence{T}"/> into a <see cref="ReadOnlySpan{T}"/>.
    /// </summary>
    /// <returns>The <see cref="ReadOnlySpan{T}"/> that wraps the <see cref="FrozenSequence{T}"/>.</returns>
    public ReadOnlySpan<T> AsSpan() => _items.AsSpan();

    /// <summary>
    /// Wraps the <see cref="FrozenSequence{T}"/> into a <see cref="ReadOnlyMemory{T}"/>.
    /// </summary>
    /// <returns>The <see cref="ReadOnlyMemory{T}"/> that wraps the <see cref="FrozenSequence{T}"/>.</returns>
    public ReadOnlyMemory<T> AsMemory() => _items.AsMemory();

    /// <summary>
    /// Returns an async safe enumerator that iterates through the sequence from the beginning to the end.
    /// </summary>
    /// <returns>An enumerator that can be used to iterate through the sequence.</returns>
    public Enumerable AsEnumerable() => new(_items, _itemsCount);

    /// <summary>
    /// Creates new array from the sequence.
    /// </summary>
    /// <returns>New array that contains the elements from the sequence.</returns>
    public T[] ToArray() => _items.ToArray();

    /// <summary>
    /// Copies the elements of the sequence to an array, starting at a particular array index.
    /// </summary>
    /// <param name="array">The one-dimensional array that is the destination of the elements copied from the sequence.</param>
    /// <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
    public void CopyTo(T[] array, int arrayIndex) => _items.CopyTo(array, arrayIndex);

    /// <summary>
    /// Copies the elements of the sequence to a <see cref="Span{T}"/>.
    /// </summary>
    /// <param name="span">The span to copy the elements to.</param>
    public void CopyTo(Span<T> span) => _items.CopyTo(span);

    /// <summary>
    /// Copies the elements of the sequence to a <see cref="Memory{T}"/>.
    /// </summary>
    /// <param name="memory">The memory to copy the elements to.</param>
    public void CopyTo(Memory<T> memory) => _items.CopyTo(memory);
}
