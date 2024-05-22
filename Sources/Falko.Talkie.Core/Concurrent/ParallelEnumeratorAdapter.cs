using System.Collections;

namespace Talkie.Concurrent;

internal sealed class ParallelEnumeratorAdapter<T>(IParallelEnumerator<T> parallelEnumerator) : IEnumerator<T>
{
    private T? _current;

    public T Current => _current!;

    object IEnumerator.Current => _current!;

    public bool MoveNext() => parallelEnumerator.TryMoveNext(out _current);

    public void Reset() => throw new NotSupportedException();

    public void Dispose() { }
}
