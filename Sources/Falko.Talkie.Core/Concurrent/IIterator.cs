using System.Diagnostics.CodeAnalysis;

namespace Talkie.Concurrent;

public interface IIterator<T>
{
    bool TryMoveNext([MaybeNullWhen(false)] out T item);
}
