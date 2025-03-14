namespace Talkie.Sequences;

public interface ISequence<T> : IReadOnlySequence<T>
{
    void Add(T value);
}
