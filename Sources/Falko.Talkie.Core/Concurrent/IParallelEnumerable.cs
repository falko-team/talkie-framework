namespace Talkie.Concurrent;

/// <summary>
/// Represents a parallel enumerable. This enumerable can be enumerated in parallel.
/// </summary>
/// <typeparam name="T">The type of the elements in the collection.</typeparam>
public interface IParallelEnumerable<T>
{
    /// <summary>
    /// Creates a parallel enumerator for the collection.
    /// </summary>
    /// <returns>A parallel enumerator that can be used to enumerate the collection in parallel.</returns>
    IParallelEnumerator<T> GetParallelEnumerator();
}
