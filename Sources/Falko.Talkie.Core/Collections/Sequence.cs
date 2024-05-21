using System.Collections;
using Talkie.Concurrent;

namespace Talkie.Collections;

public partial class Sequence<T> : ISequence<T>
{
    private Node? _first;

    private Node? _last;

    private int _count;

    public int Count => _count;

    public void Add(T value)
    {
        var next = new Node(value);

        if (_first is null)
        {
            _last = _first = next;
        }
        else
        {
            _last = _last!.Next = next;
        }

        ++_count;
    }

    public Enumerator GetEnumerator() => new(_first);

    IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IIterator<T> GetIterator() => new Iterator(_first);
}
