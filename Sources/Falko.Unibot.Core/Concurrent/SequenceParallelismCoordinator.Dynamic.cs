using Falko.Unibot.Collections;

namespace Falko.Unibot.Concurrent;

public static partial class SequenceParallelismCoordinator<T>
{
    public readonly struct Dynamic(IReadOnlySequence<T> sequence, ParallelismMeter meter)
    {
        public readonly IReadOnlySequence<T> Sequence = sequence;

        public readonly ParallelismMeter Meter = meter;
    }
}
