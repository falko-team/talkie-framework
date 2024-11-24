using System.Collections;

namespace Talkie.Concurrent;

internal sealed partial class ParallelEnumerablePartitioner<T>
{
    private sealed class Enumerable(IParallelEnumerator<T> parallelEnumerator) : IEnumerable<T>
    {
        public IEnumerator<T> GetEnumerator() => parallelEnumerator.ToEnumerable();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
