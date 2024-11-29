using System.Collections;
using System.Diagnostics;
using Talkie.Concurrent;

namespace Talkie.Sequences;

[DebuggerDisplay("Count = {Count}")]
public partial class FrozenSequence<T> : IReadOnlySequence<T>
{
    private readonly T[] _items;

    private readonly int _itemsCount;

    public FrozenSequence(IEnumerable<T> values)
    {
        _items = values.ToArray();
        _itemsCount = _items.Length;
    }

    public int Count => _itemsCount;

    public StackEnumerator GetEnumerator() => new(_items, _itemsCount);

    IEnumerator<T> IEnumerable<T>.GetEnumerator() => new HeapEnumerator(_items, _itemsCount);

    IEnumerator IEnumerable.GetEnumerator() => new HeapEnumerator(_items, _itemsCount);

    public IParallelEnumerator<T> GetParallelEnumerator() => new ParallelEnumerator(_items, _itemsCount);

    public override string ToString() => $"[{string.Join(", ", _items)}]";
}
