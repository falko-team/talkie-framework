namespace Falko.Talkie.Concurrent;

public static class IteratorExtensions
{
    public static IEnumerator<T> ToEnumerable<T>(this IIterator<T> iterator)
    {
        return new IteratorEnumerator<T>(iterator);
    }
}
