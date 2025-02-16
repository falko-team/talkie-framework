using System.Collections;
using System.Diagnostics;
using Talkie.Concurrent;

namespace Talkie.Sequences;

[DebuggerDisplay("Count = {Count}")]
public partial class FrozenSequence<T> : IReadOnlySequence<T>
{
    internal FrozenSequence(T[] items, int itemsCount) => (_items, _itemsCount) = (items, itemsCount);

    private readonly T[] _items;

    private readonly int _itemsCount;

    public int Count => _itemsCount;

    public T this[int index] => (uint)index < _itemsCount
        ? _items[index]
        : throw new IndexOutOfRangeException();

    public StructEnumerator GetEnumerator() => new(_items, _itemsCount);

    IEnumerator<T> IEnumerable<T>.GetEnumerator() => new Enumerator(_items, _itemsCount);

    IEnumerator IEnumerable.GetEnumerator() => new Enumerator(_items, _itemsCount);

    public IParallelEnumerator<T> GetParallelEnumerator() => new ParallelEnumerator(_items, _itemsCount);

    public override string ToString() => $"[{string.Join(", ", _items)}]";

    public static implicit operator FrozenSequence<T>(T item) => new([item], 1);

    public static implicit operator FrozenSequence<T>(T[] items) => new(items, items.Length);
}
