using Falko.Talkie.Sequences;

namespace Falko.Talkie.Concurrent;

public static partial class SequenceParallelismCoordinator<T>
{
    public readonly record struct Dynamic(IReadOnlySequence<T> Sequence, IParallelismMeter Meter);
}
