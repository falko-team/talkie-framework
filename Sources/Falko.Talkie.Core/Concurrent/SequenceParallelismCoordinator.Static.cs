using Falko.Talkie.Sequences;

namespace Falko.Talkie.Concurrent;

public static partial class SequenceParallelismCoordinator<T>
{
    public readonly record struct Static(IReadOnlySequence<T> Sequence);
}
