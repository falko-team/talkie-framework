using Falko.Talkie.Concurrent;

namespace Falko.Talkie.Sequences;

public interface IReadOnlySequence<T> : IReadOnlyCollection<T>, IParallelEnumerable<T>;
