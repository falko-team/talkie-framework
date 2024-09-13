using Talkie.Concurrent;

namespace Talkie.Sequences;

public interface IReadOnlySequence<T> : IReadOnlyCollection<T>, IParallelEnumerable<T>;
