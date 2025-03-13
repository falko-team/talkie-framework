using Talkie.Sequences;

namespace Talkie.Concurrent;

public static partial class SequenceParallelismCoordinator<T>
{
    public readonly record struct Dynamic(IReadOnlySequence<T> Sequence, IParallelismMeter Meter);
}
