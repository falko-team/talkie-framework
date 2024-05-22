using System.Diagnostics.CodeAnalysis;

namespace Talkie.Concurrent;

public interface IParallelEnumerator<T>
{
    bool TryMoveNext([MaybeNullWhen(false)] out T item);
}
