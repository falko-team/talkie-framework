using System.Diagnostics.CodeAnalysis;

namespace Falko.Talkie.Concurrent;

public interface IIterator<T>
{
    bool TryMoveNext([MaybeNullWhen(false)] out T item);
}
