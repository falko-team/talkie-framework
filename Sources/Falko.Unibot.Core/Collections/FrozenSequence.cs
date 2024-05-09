using System.Collections;
using Falko.Unibot.Concurrent;

namespace Falko.Unibot.Collections;

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

    public Enumerator GetEnumerator() => new(_items, _itemsCount);

    IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IIterator<T> GetIterator() => new Iterator(_items, _itemsCount);
}
