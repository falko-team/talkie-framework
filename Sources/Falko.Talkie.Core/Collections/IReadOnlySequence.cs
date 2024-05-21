using Falko.Talkie.Concurrent;

namespace Falko.Talkie.Collections;

public interface IReadOnlySequence<T> : IReadOnlyCollection<T>, IIterable<T>;
