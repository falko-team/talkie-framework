namespace Talkie.Sequences;

public partial interface ISequence<T> : IReadOnlySequence<T>
{
    void Add(T value);
}
