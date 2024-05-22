using Talkie.Concurrent;

namespace Talkie.Collections;

public interface IReadOnlySequence<T> : IReadOnlyCollection<T>, IParallelEnumerable<T>;
