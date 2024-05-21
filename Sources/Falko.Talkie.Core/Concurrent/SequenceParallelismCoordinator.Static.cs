using Talkie.Collections;

namespace Talkie.Concurrent;

public static partial class SequenceParallelismCoordinator<T>
{
    public readonly struct Static(IReadOnlySequence<T> sequence)
    {
        public readonly IReadOnlySequence<T> Sequence = sequence;
    }
}
