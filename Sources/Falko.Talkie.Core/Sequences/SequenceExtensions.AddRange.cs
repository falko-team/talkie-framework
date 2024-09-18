using System.Runtime.CompilerServices;

namespace Talkie.Sequences;

public static partial class SequenceExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddRange<T>(this ISequence<T> sequence, IEnumerable<T> values)
    {
        foreach (var value in values)
        {
            sequence.Add(value);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddRange<T>(this ISequence<T> sequence, IReadOnlyCollection<T> values)
    {
        if (values.Count is 0) return;

        foreach (var value in values)
        {
            sequence.Add(value);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddRange<T>(this ISequence<T> sequence, Sequence<T> values)
    {
        if (values.Count is 0) return;

        foreach (var value in values)
        {
            sequence.Add(value);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddRange<T>(this ISequence<T> sequence, RemovableSequence<T> values)
    {
        if (values.Count is 0) return;

        foreach (var value in values)
        {
            sequence.Add(value);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddRange<T>(this ISequence<T> sequence, FrozenSequence<T> values)
    {
        if (values.Count is 0) return;

        foreach (var value in values)
        {
            sequence.Add(value);
        }
    }
}
