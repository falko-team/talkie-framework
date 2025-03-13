using Talkie.Sequences;

namespace Talkie.Concurrent;

public static partial class SequenceParallelismCoordinator<T>
{
    public readonly record struct Static(IReadOnlySequence<T> Sequence);
}
