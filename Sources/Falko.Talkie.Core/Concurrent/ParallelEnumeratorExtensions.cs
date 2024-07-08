namespace Talkie.Concurrent;

public static partial class ParallelEnumeratorExtensions
{
    public static IEnumerator<T> ToEnumerable<T>(this IParallelEnumerator<T> parallelEnumerator)
    {
        return new ParallelEnumeratorAdapter<T>(parallelEnumerator);
    }
}
