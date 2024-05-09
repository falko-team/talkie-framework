namespace Falko.Unibot.Concurrent;

public interface IIterable<T>
{
    new IIterator<T> GetIterator();
}
