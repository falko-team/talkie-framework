namespace Talkie.Concurrent;

public interface IIterable<T>
{
    new IIterator<T> GetIterator();
}
