using System.Collections;
using System.Runtime.CompilerServices;

namespace Talkie.Concurrent;

[method: MethodImpl(MethodImplOptions.AggressiveInlining)]
internal struct ParallelEnumeratorAdapter<T>(IParallelEnumerator<T> parallelEnumerator) : IEnumerator<T>
{
    private T? _current;

    public T Current
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _current!;
    }

    object IEnumerator.Current
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _current!;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool MoveNext() => parallelEnumerator.TryMoveNext(out _current);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset() => throw new NotSupportedException();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Dispose() { }
}
