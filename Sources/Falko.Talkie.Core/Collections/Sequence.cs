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

        _last = _first is null
            ? _first = next
            : _last!.Next = next;

        ++_count;
    }

    public Enumerator GetEnumerator() => new(_first);

    IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IParallelEnumerator<T> GetParallelEnumerator() => new ParallelEnumerator(_first);

    public override string ToString() => $"[{string.Join(", ", this)}]";
}
