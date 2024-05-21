namespace Talkie.Collections;

public partial interface ISequence<T> : IReadOnlySequence<T>
{
    void Add(T value);
}
