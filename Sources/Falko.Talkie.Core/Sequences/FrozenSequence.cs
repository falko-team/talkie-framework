using System.Collections;
using System.Diagnostics;
using Talkie.Concurrent;

namespace Talkie.Sequences;

[DebuggerDisplay("Count = {Count}")]
public partial class FrozenSequence<T> : IReadOnlySequence<T>
{
    private readonly T[] _items;

    private readonly int _itemsCount;

    public int Count => _itemsCount;

    public StructEnumerator GetEnumerator() => new(_items, _itemsCount);

    IEnumerator<T> IEnumerable<T>.GetEnumerator() => new Enumerator(_items, _itemsCount);

    IEnumerator IEnumerable.GetEnumerator() => new Enumerator(_items, _itemsCount);

    public IParallelEnumerator<T> GetParallelEnumerator() => new ParallelEnumerator(_items, _itemsCount);

    public override string ToString() => $"[{string.Join(", ", _items)}]";
}
