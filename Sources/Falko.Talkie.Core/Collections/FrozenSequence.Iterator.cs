using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Talkie.Concurrent;

namespace Talkie.Collections;

public partial class FrozenSequence<T>
{
    [method: MethodImpl(MethodImplOptions.AggressiveInlining)]
    private sealed class Iterator(T[] items, int itemsCount) : IIterator<T>
    {
        private int _lastItemIndex = -1;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryMoveNext([MaybeNullWhen(false)] out T item)
        {
            var currentItemIndex = Interlocked.Increment(ref _lastItemIndex);

            if (currentItemIndex < itemsCount)
            {
                item = Unsafe.Add(ref MemoryMarshal.GetArrayDataReference(items), currentItemIndex);
                return true;
            }

            item = default;
            return false;
        }
    }
}
