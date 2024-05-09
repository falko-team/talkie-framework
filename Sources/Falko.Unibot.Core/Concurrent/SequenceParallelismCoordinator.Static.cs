using Falko.Unibot.Collections;

namespace Falko.Unibot.Concurrent;

public static partial class SequenceParallelismCoordinator<T>
{
    public readonly struct Static(IReadOnlySequence<T> sequence)
    {
        public readonly IReadOnlySequence<T> Sequence = sequence;
    }
}
