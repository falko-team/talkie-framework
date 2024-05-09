using Falko.Unibot.Concurrent;

namespace Falko.Unibot.Collections;

public interface IReadOnlySequence<T> : IReadOnlyCollection<T>, IIterable<T>;
