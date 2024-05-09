using System.Diagnostics.CodeAnalysis;

namespace Falko.Unibot.Concurrent;

public interface IIterator<T>
{
    bool TryMoveNext([MaybeNullWhen(false)] out T item);
}
