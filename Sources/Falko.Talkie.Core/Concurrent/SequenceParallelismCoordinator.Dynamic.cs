using Falko.Talkie.Collections;

namespace Falko.Talkie.Concurrent;

public static partial class SequenceParallelismCoordinator<T>
{
    public readonly struct Dynamic(IReadOnlySequence<T> sequence, ParallelismMeter meter)
    {
        public readonly IReadOnlySequence<T> Sequence = sequence;

        public readonly ParallelismMeter Meter = meter;
    }
}
