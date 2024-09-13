using System.Collections;
using Talkie.Concurrent;

namespace Talkie.Sequences;

public partial class RemovableSequence<T> : ISequence<T>
{
    private Node? _first;

    private int _count;

    public int Count => _count;

    public Remover Add(T value)
    {
        ++_count;

        var next = new Node(value);

        if (_first is null)
        {
            _first = next.Previous = next;
        }
        else
        {
            next.Previous = _first.Previous;
            _first.Previous = _first.Previous!.Next = next;
        }

        return new Remover(this, next);
    }

    void ISequence<T>.Add(T value) => Add(value);

    public Enumerator GetEnumerator() => new(_first);

    IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IParallelEnumerator<T> GetParallelEnumerator() => new ParallelEnumerator(_first);

    public override string ToString() => $"[{string.Join(", ", this)}]";
}
