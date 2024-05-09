using System.Collections;

namespace Falko.Unibot.Concurrent;

internal sealed class IteratorEnumerator<T>(IIterator<T> iterator) : IEnumerator<T>
{
    private T? _current;

    public T Current => _current!;

    object IEnumerator.Current => _current!;

    public bool MoveNext() => iterator.TryMoveNext(out _current);

    public void Reset() => throw new NotSupportedException();

    public void Dispose() { }
}
