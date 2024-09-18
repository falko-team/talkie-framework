using System.Runtime.CompilerServices;

namespace Talkie.Sequences;

public static partial class SequenceExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ForEach<T>(this IReadOnlySequence<T> sequence, Action<T> action) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(sequence);
        ArgumentNullException.ThrowIfNull(action);

        if (sequence.Count is 0) return;

        foreach (var value in sequence)
        {
            action(value);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ForEach<T>(this Sequence<T> sequence, Action<T> action) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(sequence);
        ArgumentNullException.ThrowIfNull(action);

        if (sequence.Count is 0) return;

        foreach (var value in sequence)
        {
            action(value);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ForEach<T>(this RemovableSequence<T> sequence, Action<T> action) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(sequence);
        ArgumentNullException.ThrowIfNull(action);

        if (sequence.Count is 0) return;

        foreach (var value in sequence)
        {
            action(value);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ForEach<T>(this FrozenSequence<T> sequence, Action<T> action) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(sequence);
        ArgumentNullException.ThrowIfNull(action);

        if (sequence.Count is 0) return;

        foreach (var value in sequence)
        {
            action(value);
        }
    }
}
