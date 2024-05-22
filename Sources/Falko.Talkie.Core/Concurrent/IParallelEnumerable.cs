namespace Talkie.Concurrent;

public interface IParallelEnumerable<T>
{
    IParallelEnumerator<T> GetParallelEnumerator();
}
