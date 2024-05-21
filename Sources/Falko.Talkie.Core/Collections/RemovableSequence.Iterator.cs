using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Falko.Talkie.Concurrent;

namespace Falko.Talkie.Collections;

public partial class RemovableSequence<T>
{
    [method: MethodImpl(MethodImplOptions.AggressiveInlining)]
    private sealed class Iterator(Node? first) : IIterator<T>
    {
        private Node? _current = first;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryMoveNext([MaybeNullWhen(false)] out T item)
        {
            while (Interlocked.CompareExchange(ref _current, _current?.Next, _current) is { } current)
            {
                item = current.Value;
                return true;
            }

            item = default;
            return false;
        }
    }
}
