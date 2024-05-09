namespace Falko.Unibot.Collections;

public partial interface ISequence<T> : IReadOnlySequence<T>
{
    void Add(T value);
}
