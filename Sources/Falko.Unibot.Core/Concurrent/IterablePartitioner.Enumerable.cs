using System.Collections;

namespace Falko.Unibot.Concurrent;

internal sealed partial class IterablePartitioner<T>
{
    private sealed class Enumerable(IIterator<T> iterator) : IEnumerable<T>
    {
        public IEnumerator<T> GetEnumerator() => iterator.ToEnumerable();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
