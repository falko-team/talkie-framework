namespace Talkie.Sequences;

public static partial class FrozenSequence
{
    public static FrozenSequence<T> Wrap<T>(params T[] items) => new(items, items.Length);

    public static FrozenSequence<T> Wrap<T>(T item) => new([item], 1);
}
