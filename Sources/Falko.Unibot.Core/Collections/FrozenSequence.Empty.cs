namespace Falko.Unibot.Collections;

public partial class FrozenSequence<T>
{
    private FrozenSequence() => _items = [];

    public static FrozenSequence<T> Empty => EmptyCache.Instance;

    private static class EmptyCache
    {
        public static readonly FrozenSequence<T> Instance = new();
    }
}
