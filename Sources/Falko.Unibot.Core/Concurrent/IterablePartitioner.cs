using System.Collections.Concurrent;

namespace Falko.Unibot.Concurrent;

internal sealed partial class IterablePartitioner<T>(IIterable<T> iterable) : Partitioner<T>
{
    public override bool SupportsDynamicPartitions => true;

    public override IList<IEnumerator<T>> GetPartitions(int partitionCount) => throw new NotSupportedException();

    public override IEnumerable<T> GetDynamicPartitions() => new Enumerable(iterable.GetIterator());
}
