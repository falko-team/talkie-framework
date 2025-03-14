using System.Diagnostics.CodeAnalysis;

namespace Talkie.Concurrent;

/// <summary>
/// Represents a parallel enumerator. This enumerator can be enumerated in parallel.
/// </summary>
/// <typeparam name="T">The type of the elements in the collection.</typeparam>
public interface IParallelEnumerator<T>
{
    /// <summary>
    /// Try to move the enumerator to the next element in the collection.
    /// </summary>
    /// <param name="item">
    /// The next element in the collection if returned <see langword="true"/>;
    /// or the default value of <typeparamref name="T"/> if returned <see langword="false"/>.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the enumerator was successfully moved to the next element;
    /// <see langword="false"/> if the enumerator has passed the end of the collection.
    /// </returns>
    bool TryMoveNext([MaybeNullWhen(false)] out T item);
}
