namespace Talkie.Sequences;

public partial class FrozenSequence
{
    public static FrozenSequence<T> Empty<T>()
    {
        return EmptyCache<T>.Instance;
    }

    private static class EmptyCache<T>
    {
        public static readonly FrozenSequence<T> Instance = new([], 0);
    }
}
