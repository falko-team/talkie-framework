using System.Runtime.CompilerServices;

namespace Talkie.Sequences;

[method: MethodImpl(MethodImplOptions.AggressiveInlining)]
public readonly ref struct SequenceAdapter<T>(IEnumerable<T> sequence)
{
    public readonly IEnumerable<T> Sequence = sequence;
}
